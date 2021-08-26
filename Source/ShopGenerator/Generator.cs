using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class Generator : Singleton<Generator>
	{
		private string _shopRep = "";

		public Generator(string shopRep) : base()
		{
			_instance = this;
			_shopRep = shopRep;
		}

		public void GenerateShop(MainWindow window)
		{
			if(Directory.Exists(_shopRep) == false)
			{
				Directory.CreateDirectory(_shopRep);
			}
			string shopPath = _shopRep + "/" + window.ShopName + ".shp";

			Database.Instance.GenerateShop(shopPath, window.OwnerName, window.Description, CreateGeneralCriteria(window), CreateLimitationNumbers(window));
			
			//Open the shop file
			System.Diagnostics.Process.Start(shopPath);
		}

		private List<Criteria> CreateGeneralCriteria(MainWindow window)
		{
			List<Criteria> listCriteria = new List<Criteria>();

			//Illegal
			listCriteria.Add(new CriteriaIllegality(ElementType.Invalid, window.Illegal));
			
			//Price
			if(window.Price.Min > 0)
			{
				listCriteria.Add(new Criteria(ElementType.Invalid, PropertyType.Price, Criteria.ComparaisonType.Superior, window.Price.Min.ToString()));
			}
			if(window.Price.Max > 0)
			{
				listCriteria.Add(new Criteria(ElementType.Invalid, PropertyType.Price, Criteria.ComparaisonType.Inferior, window.Price.Max.ToString()));
			}

			//Rarity
			if(window.Rarity.Min > 0)
			{
				listCriteria.Add(new Criteria(ElementType.Invalid, PropertyType.Rarity, Criteria.ComparaisonType.Superior, window.Rarity.Min.ToString()));
			}
			if(window.Rarity.Max > 0)
			{
				listCriteria.Add(new Criteria(ElementType.Invalid, PropertyType.Rarity, Criteria.ComparaisonType.Inferior, window.Rarity.Max.ToString()));
			}

			//Name filter
			if(string.IsNullOrEmpty(window.NameFilter) == false)
			{
				listCriteria.Add(new CriteriaStringContains(ElementType.Invalid, PropertyType.Name, Criteria.ComparaisonType.Equal, window.NameFilter));
			}

			//Add filter by elementType
			foreach(ElementType elementType in Enum.GetValues(typeof(ElementType)))
			{
				CategoryConfiguration categoryConfiguration = window.GetCategoryConfiguration(elementType);
				if(categoryConfiguration != null)
				{
					if (categoryConfiguration.Rarity.Min > 0)
					{
						listCriteria.Add(new Criteria(elementType, PropertyType.Rarity, Criteria.ComparaisonType.Superior, categoryConfiguration.Rarity.Min.ToString()));
					}
					if (categoryConfiguration.Rarity.Max > 0)
					{
						listCriteria.Add(new Criteria(elementType, PropertyType.Rarity, Criteria.ComparaisonType.Inferior, categoryConfiguration.Rarity.Max.ToString()));
					}

					if (categoryConfiguration.Illegal > window.Illegal)
					{
						listCriteria.Add(new CriteriaIllegality(elementType, categoryConfiguration.Illegal));
					}
				}
			}

			return listCriteria;
		}

		private List<LimitationNumber> CreateLimitationNumbers(MainWindow window)
		{
			List<LimitationNumber> listLimitationNumber = new List<LimitationNumber>();

			listLimitationNumber.Add(new LimitationNumber(null, null, window.NbArticles.Min, window.NbArticles.Max));

			listLimitationNumber.AddRange(window.CategoryConfigurationWeapon.CreateLimitationNumberList());
			listLimitationNumber.AddRange(window.CategoryConfigurationArmor.CreateLimitationNumberList());
			listLimitationNumber.AddRange(window.CategoryConfigurationGear.CreateLimitationNumberList());
			listLimitationNumber.AddRange(window.CategoryConfigurationAttachment.CreateLimitationNumberList());
			listLimitationNumber.AddRange(window.CategoryConfigurationBlackMarket.CreateLimitationNumberList());
			return listLimitationNumber;
		}

		private List<Criteria> CreateTypeCriteria(ElementType typeCriteria)
		{
			return null;
		}
	}
}
