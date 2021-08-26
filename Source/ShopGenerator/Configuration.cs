using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopGenerator
{
	[Serializable]
	public class Configuration
	{
#if DEBUG && DEBUG_REP
        private const string _directoryName = "I:/JDR/Star Wars/Edge/Outils/ShopGenerator/Source/ShopGenerator/ShopConfig/";
#else
		private const string _directoryName = "ShopConfig";
#endif
		private List<Type> _listExtraType = new List<Type>();

		private string _shopName = "Enter the shop name";
		private string _ownerName = "Name";
		private string _description = "Description";
		private Illegality _illegal = Illegality.Legal;
		private ValueMinMax _price = new ValueMinMax(100, 100000);
		private ValueMinMax _rarity = new ValueMinMax(1, 10);
		private string _nameFilter = "";
		private ValueMinMax _nbArticles = new ValueMinMax(10, 100);
		private List<CategoryConfiguration> _listCategoryConfiguration = new List<CategoryConfiguration>();
		private string _directoryPath = "";

		//Accessors
		public static string DirectoryName { get { return _directoryName; } }
		public string ShopName { get { return _shopName; } set { _shopName = value; } }
		public string OwnerName { get { return _ownerName; } set { _ownerName = value; } }
		public string Description { get { return _description; } set { _description = value; } }
		public Illegality Illegal { get { return _illegal; } set { _illegal = value; } }
		public ValueMinMax Price { get { return _price; } set { _price = value; } }
		public ValueMinMax Rarity { get { return _rarity; } set { _rarity = value; } }
		public ValueMinMax NbArticles { get { return _nbArticles; } set { _nbArticles = value; } }
		public string NameFilter { get { return _nameFilter; } set { _nameFilter = value; } }
		public List<CategoryConfiguration> ListCategoryConfiguration { get { return _listCategoryConfiguration; } set { _listCategoryConfiguration = value; } }

		public CategoryConfigurationWeapon CategoryConfigurationWeapon { get { return GetCategoryConfiguration(ElementType.Weapon) as CategoryConfigurationWeapon; } }
		public CategoryConfigurationArmor CategoryConfigurationArmor { get { return GetCategoryConfiguration(ElementType.Armor) as CategoryConfigurationArmor; } }
		public CategoryConfigurationGear CategoryConfigurationGear { get { return GetCategoryConfiguration(ElementType.Gear) as CategoryConfigurationGear; } }
		public CategoryConfigurationBlackMarket CategoryConfigurationBlackMarket { get { return GetCategoryConfiguration(ElementType.BlackMarket) as CategoryConfigurationBlackMarket; } }
		public CategoryConfigurationAttachment CategoryConfigurationAttachment { get { return GetCategoryConfiguration(ElementType.Attachment) as CategoryConfigurationAttachment; } }

		public Configuration()
		{
			_listExtraType.Clear();
			_listExtraType.Add(typeof(CategoryConfigurationWeapon));
			_listExtraType.Add(typeof(CategoryConfigurationArmor));
			_listExtraType.Add(typeof(CategoryConfigurationGear));
			_listExtraType.Add(typeof(CategoryConfigurationBlackMarket));
			_listExtraType.Add(typeof(CategoryConfigurationAttachment));
			_directoryPath = Directory.GetCurrentDirectory() + _directoryName + "/";
		}

		public void Init()
		{
			_listCategoryConfiguration.Add(new CategoryConfigurationWeapon(ElementType.Weapon));
			_listCategoryConfiguration.Add(new CategoryConfigurationArmor(ElementType.Armor));
			_listCategoryConfiguration.Add(new CategoryConfigurationGear(ElementType.Gear));
			_listCategoryConfiguration.Add(new CategoryConfigurationBlackMarket(ElementType.BlackMarket));
			_listCategoryConfiguration.Add(new CategoryConfigurationAttachment(ElementType.Attachment));
		}

		public CategoryConfiguration GetCategoryConfiguration(ElementType elementType)
		{
			foreach (CategoryConfiguration categoryConfiguration in _listCategoryConfiguration)
			{
				if (categoryConfiguration.ElementType == elementType)
				{
					return categoryConfiguration;
				}
			}
			return null;
		}

		public void Save()
		{
			//Save to filename
			_WriteDataInFileXml(_directoryName, _shopName + ".cfg", this, typeof(Configuration));

			Process.Start(_directoryName);
		}

		public void Load(string shopName)
		{
			Configuration configuration = _ReadDataInFileXml(_directoryName + "/" + _shopName + ".cfg", typeof(Configuration)) as Configuration;
			if (configuration != null)
			{
				Copy(configuration);
			}
		}

		protected void Copy(Configuration configuration)
		{
			_shopName = configuration._shopName;
			_ownerName = configuration._ownerName;
			_description = configuration._description;
			_illegal = configuration._illegal;
			_price = configuration._price;
			_rarity = configuration._rarity;
			_nameFilter = configuration._nameFilter;
			_nbArticles = configuration._nbArticles;
			_listCategoryConfiguration = configuration._listCategoryConfiguration;
		}

		protected object _ReadDataInFileXml(string filePath, Type typeofData)
		{
			object saveData = null;
			if (File.Exists(filePath))
			{
				XmlSerializer bf = new XmlSerializer(typeofData, _listExtraType.ToArray());

				FileStream file = File.Open(filePath, FileMode.Open);

				saveData = bf.Deserialize(file);
				file.Close();
			}
			return saveData;
		}

		private void _WriteDataInFileXml(string directory, string fileName, object saveData, Type typeofData)
		{
			if (Directory.Exists(directory) == false)
			{
				Directory.CreateDirectory(directory);
			}
			XmlSerializer bfSpecific = new XmlSerializer(typeofData, _listExtraType.ToArray());
			FileStream fileSpecific = File.Create(directory + "/" + fileName);

			bfSpecific.Serialize(fileSpecific, saveData);
			fileSpecific.Close();
		}
	}
}
