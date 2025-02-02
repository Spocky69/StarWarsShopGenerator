using ShopGenerator.Base;
using StarWarsShopGenerator.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ShopGenerator
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : INotifyPropertyChanged
	{
		public const string DEFAULT_FILE_NAME = "Nouveau Shop";
		static public MainWindow Instance;

		private string _shopFileName;
		private Configuration _configuration = new Configuration(true);
		private List<string> _allShopNameFiles = new List<string>();
		private string _directoryPath = "";

		#region Accessors
		public List<string> AllShopNameFiles
		{
			get { return _allShopNameFiles; }
			set
			{
				if (_allShopNameFiles != value)
				{
					_allShopNameFiles = value;
					OnPropertyChanged("AllShopNameFiles");
				}
			}
		}

		public string ShopFileName
		{
			get { return _shopFileName; }
			set
			{
				if (_shopFileName != value)
				{
					_shopFileName = value;
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

		#region Properties
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Properties

		public MainWindow()
		{
			string shopConfigDirectory = "";
			shopConfigDirectory = Directory.GetCurrentDirectory() + "/../";
			shopConfigDirectory += "ShopConfig/";

			if (Directory.Exists(shopConfigDirectory) == false)
			{
				shopConfigDirectory = Service.GetApplicationUserPath() + "/ShopConfig/";
			}

			_directoryPath = shopConfigDirectory;

			Instance = this;
			DataContext = this;

			//Fill from all the cfg 
			if (Directory.Exists(_directoryPath))
			{
				string[] allFiles = Directory.GetFiles(_directoryPath);
				for (int i = 0; i < allFiles.Length; i++)
				{
					string fileName = System.IO.Path.GetFileNameWithoutExtension(allFiles[i]);
					if (_allShopNameFiles.Contains(fileName) == false)
					{
						_allShopNameFiles.Add(fileName);
					}
				}
			}

			_allShopNameFiles.Sort();

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
			if (_shopFileName != "")
			{
				Generator.Instance.GenerateShop(this);
			}
		}

		private void ButtonSauvegarde_Click(object sender, RoutedEventArgs e)
		{
			_configuration.Save(_directoryPath, _shopFileName);
			if (_allShopNameFiles.Contains(_shopFileName) == false)
			{
				List<string> allShopNames = new List<string>();
				allShopNames.AddRange(_allShopNameFiles);
				allShopNames.Add(_shopFileName);
				allShopNames.Sort();
				AllShopNameFiles = allShopNames;
			}
		}

		private void ButtonCodeBook_Click(object sender, RoutedEventArgs e)
		{
			Database.Instance.OpenCodeBook();
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_configuration.Load(_directoryPath, _shopFileName);
			OnPropertyChanged(propertyName: null);
		}

		private void ButtonSuppression_Click(object sender, RoutedEventArgs e)
		{
			ValidationWindow validationWindow = new ValidationWindow();
			validationWindow.Top = Instance.Top + 720;
			validationWindow.Left = Instance.Left + 85;
			validationWindow.ShowDialog();

			if (validationWindow.Success)
			{
				int indexCurrentShop = _allShopNameFiles.FindIndex(x => x == _shopFileName);
				indexCurrentShop = Math.Max(indexCurrentShop - 1, 0);

				List<string> allShopNames = new List<string>();
				allShopNames.AddRange(_allShopNameFiles);

				allShopNames.Remove(_shopFileName);
				_configuration.Delete(_directoryPath, _shopFileName);

				AllShopNameFiles = allShopNames;

				if (allShopNames.Count > 0)
				{
					ShopFileName = allShopNames[indexCurrentShop];
					_configuration.Load(_directoryPath, _shopFileName);
				}
				else
				{
					ShopFileName = DEFAULT_FILE_NAME;
					_configuration = new Configuration(true);
				}

				OnPropertyChanged();
			}
		}

		private void ButtonNew_Click(object sender, RoutedEventArgs e)
		{
			NewFileWindow newFileWindow = new NewFileWindow();
			newFileWindow.TxtInput.Focus();
			newFileWindow.TxtInput.SelectionStart = newFileWindow.TxtInput.Text.Length;
			newFileWindow.Top = Instance.Top + 700;
			newFileWindow.Left = Instance.Left + 20;
			newFileWindow.ShowDialog();
			if (newFileWindow.Success)
			{
				string newFileName = newFileWindow.FileName;
				if (string.IsNullOrEmpty(newFileName) == false)
				{
					_configuration = new Configuration(true);
					AllShopNameFiles.Add(newFileName);
					AllShopNameFiles.Sort();
					List<string> allShopNames = new List<string>();
					allShopNames.AddRange(AllShopNameFiles);
					AllShopNameFiles = allShopNames;
					ShopFileName = newFileName;
					ShopName = newFileName;
					OnPropertyChanged();
				}
			}
		}

		private void ButtonCopy_Click(object sender, RoutedEventArgs e)
		{
			NewFileWindow newFileWindow = new NewFileWindow();
			newFileWindow.TxtInput.Focus();
			newFileWindow.TxtInput.SelectionStart = newFileWindow.TxtInput.Text.Length;
			newFileWindow.Top = Instance.Top + 700;
			newFileWindow.Left = Instance.Left + 20;
			if (string.IsNullOrEmpty(_shopFileName) == false)
			{
				newFileWindow.FileName = _shopFileName + "_Copy";
				newFileWindow.ShowDialog();

				if (newFileWindow.Success)
				{
					string newFileName = newFileWindow.FileName;
					if (string.IsNullOrEmpty(newFileName) == false)
					{
						_configuration.Save(_directoryPath, newFileName);

						AllShopNameFiles.Add(newFileName);
						AllShopNameFiles.Sort();
						List<string> allShopNames = new List<string>();
						allShopNames.AddRange(AllShopNameFiles);
						AllShopNameFiles = allShopNames;
						OnPropertyChanged();
						ShopFileName = newFileName;
					}
				}
			}
		}
	}
}
