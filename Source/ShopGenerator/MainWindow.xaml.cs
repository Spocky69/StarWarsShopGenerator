using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopGenerator
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	/// 

	public partial class MainWindow : INotifyPropertyChanged
	{
		static public MainWindow Instance;

		private Configuration _configuration = new Configuration();
		private List<string> _allShopNames = new List<string>();

		#region Accessors
		public List<string> AllShopNames
		{
			get { return _allShopNames; }
			set
			{
				if (_allShopNames != value)
				{
					_allShopNames = value;
					OnPropertyChanged();
				}
			}
		}

		public string ShopName
		{
			get { return _configuration.ShopName; }
			set
			{
				if (_configuration.ShopName != value)
				{
					_configuration.ShopName = value;
					OnPropertyChanged();
				}
			}
		}

		public string OwnerName
		{
			get { return _configuration.OwnerName; }
			set
			{
				if (_configuration.OwnerName != value)
				{
					_configuration.OwnerName = value;
					OnPropertyChanged();
				}
			}
		}

		public string Description
		{
			get { return _configuration.Description; }
			set
			{
				if (_configuration.Description != value)
				{
					_configuration.Description = value;
					OnPropertyChanged();
				}
			}
		}

		public Illegality Illegal
		{
			get { return _configuration.Illegal; }
			set
			{
				if (_configuration.Illegal != value)
				{
					_configuration.Illegal = value;
					OnPropertyChanged();
				}
			}
		}

		public ValueMinMax Price { get { return _configuration.Price; } }
		public ValueMinMax Rarity { get { return _configuration.Rarity; } }
		public ValueMinMax NbArticles { get { return _configuration.NbArticles; } }

		public string NameFilter
		{
			get { return _configuration.NameFilter; }
			set
			{
				if (_configuration.NameFilter != value)
				{
					_configuration.NameFilter = value;
					OnPropertyChanged();
				}
			}
		}

		public CategoryConfigurationWeapon CategoryConfigurationWeapon { get { return _configuration.CategoryConfigurationWeapon; } }
		public CategoryConfigurationArmor CategoryConfigurationArmor { get { return _configuration.CategoryConfigurationArmor; } }
		public CategoryConfigurationGear CategoryConfigurationGear { get { return _configuration.CategoryConfigurationGear; } }
		public CategoryConfigurationBlackMarket CategoryConfigurationBlackMarket { get { return _configuration.CategoryConfigurationBlackMarket; } }
		public CategoryConfigurationAttachment CategoryConfigurationAttachment { get { return _configuration.CategoryConfigurationAttachment; } }

		#endregion Accessors

		public event PropertyChangedEventHandler PropertyChanged;

		public MainWindow()
		{
			string shopConfigDirectory = "";
#if DEBUG
			shopConfigDirectory = Directory.GetCurrentDirectory() + "/../";
#else
			shopConfigDirectory = Service.GetApplicationUserPath();
#endif
			shopConfigDirectory += "ShopConfig/";
			_configuration.Init(shopConfigDirectory);

			Instance = this;
			DataContext = this;

			//Fill from all the cfg 
			if (Directory.Exists(_configuration.DirectoryPath))
			{
				string[] allFiles = Directory.GetFiles(_configuration.DirectoryPath);
				for (int i = 0; i < allFiles.Length; i++)
				{
					string fileName = System.IO.Path.GetFileNameWithoutExtension(allFiles[i]);
					if (_allShopNames.Contains(fileName) == false)
					{
						_allShopNames.Add(fileName);
					}
				}
			}
			_allShopNames.Sort();

			InitializeComponent();
		}

		public CategoryConfiguration GetCategoryConfiguration(ElementType elementType)
		{
			return _configuration.GetCategoryConfiguration(elementType);
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void ButtonGenerateShop_Click(object sender, RoutedEventArgs e)
		{
			if (_configuration.ShopName != "")
			{
				Generator.Instance.GenerateShop(this);
			}
		}

		private void ButtonSauvegarde_Click(object sender, RoutedEventArgs e)
		{
			_configuration.Save();
			if (_allShopNames.Contains(_configuration.ShopName) == false)
			{
				List<string> allShopNames = new List<string>();
				allShopNames.AddRange(_allShopNames);
				allShopNames.Add(_configuration.ShopName);
				allShopNames.Sort();
				AllShopNames = allShopNames;
			}
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_configuration.Load();
			OnPropertyChanged(propertyName: null);
		}

		private void ButtonSuppression_Click(object sender, RoutedEventArgs e)
		{
			int indexCurrentShop = _allShopNames.FindIndex(x => x == _configuration.ShopName);
			indexCurrentShop = Math.Max(indexCurrentShop - 1, 0);

			List<string> allShopNames = new List<string>();
			allShopNames.AddRange(_allShopNames);

			allShopNames.Remove(_configuration.ShopName);
			_configuration.Delete();

			AllShopNames = allShopNames;

			if (allShopNames.Count > 0)
			{
				_configuration.ShopName = allShopNames[indexCurrentShop];
			}
			else
			{
				_configuration.ShopName = "Entrez une nouveau nom de magasin";
			}
			_configuration.Load();

			OnPropertyChanged(propertyName: null);
		}
	}
}
