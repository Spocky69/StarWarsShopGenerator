using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopGenerator
{
	/// <summary>
	/// Logique d'interaction pour App.xaml
	/// </summary>
	public partial class App : Application
	{
		private Database _database;
		private Generator _generator;
		private Localization _localization;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			// base rep
			string baseFolder = "";
			while(string.IsNullOrEmpty(baseFolder))
			{
				if(Directory.Exists(Directory.GetCurrentDirectory()+"/Database"))
				{
					baseFolder = Directory.GetCurrentDirectory();
				}
				else
				{
					DirectoryInfo rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
					if(rootDirectory.Exists)
					{
						Directory.SetCurrentDirectory(rootDirectory.FullName);
					}
					else
					{
						break;
					}
				}
			}

			string databaseRep = Directory.GetCurrentDirectory() +  "/Database/";
			string shopRep = Service.GetApplicationUserPath() + "ShopGenerated/";

			_localization = new Localization();
			_database = new Database(databaseRep);
			_database.Init();
			_generator = new Generator(shopRep);
		}
	}
}
