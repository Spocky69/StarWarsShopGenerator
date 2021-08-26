using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class Database : Singleton<Database>
	{
		private List<ContainerElementDesc> _containerElementDescs = new List<ContainerElementDesc>();
		Localization _localization;
		private string _databaseRep = "";
		private char[] _separators = { ';'}; 

		public Database(string databaseRep) : base()
		{
			_instance = this;
			_databaseRep = databaseRep;
		}

		public void Init()
		{
			foreach(ElementType elementType in Enum.GetValues(typeof(ElementType)))
			{
				if(elementType != ElementType.Invalid)
				{
					ContainerElementDesc containerElementDesc = CreateContainerElementDesc(elementType);
					_containerElementDescs.Add(containerElementDesc);
				}
			}
			_localization = new Localization();
			_localization.Init(_databaseRep);
			
			ConstructDatabase();
		}

		public void GenerateShop(string shopPath, string ownerName, string description, List<Criteria> listCriteriaGeneral, List<LimitationNumber> listLimitationNumber)
		{
			//Filter the list
			List<ElementDesc> elementDescsFinal = new List<ElementDesc>();
			foreach (ContainerElementDesc containerElementDesc in _containerElementDescs)
			{
				List<ElementDesc> elementDescs = containerElementDesc.GenerateListWithCriteriasAndLimitationNumber(listCriteriaGeneral, listLimitationNumber);
				if(elementDescs != null)
				{
					elementDescsFinal.AddRange(elementDescs);	
				}
			}

			// Remove element from the general numbers of items in the shop
			LimitationNumber limitationNumberGeneral = LimitationNumber.GetLimitationNumberByType(null, null, listLimitationNumber);
			elementDescsFinal = limitationNumberGeneral.FilterElementDesc(elementDescsFinal, PropertyType.Invalid);

			//Write to file
			string finalString = ownerName + "\n";
			finalString += description + "\n\n";
			foreach (ContainerElementDesc containerElementDesc in _containerElementDescs)
			{
				if(elementDescsFinal != null)
				{
					finalString += containerElementDesc.GenerateLines(elementDescsFinal);
				}
			}

			finalString = Regex.Replace(finalString, "(?<!\r)\n", "\r\n");

			StreamWriter stream = File.CreateText(shopPath);
			stream.Write(finalString);
			stream.Close();
		}

		private ContainerElementDesc CreateContainerElementDesc(ElementType elementType)
		{
			switch(elementType)
			{
				case ElementType.Armor: return new ContainerElementDescArmor(elementType);
				case ElementType.Gear: return new ContainerElementDesc(elementType);
				case ElementType.Weapon: return new ContainerElementDescWeapon(elementType);
				case ElementType.Attachment: return new ContainerElementDescAttachment(elementType);
				case ElementType.BlackMarket: return new ContainerElementDesc(elementType);
				default: return null;
			}
		}

		private ContainerElementDesc GetContainerElementDesc(ElementType elementType)
		{
			foreach(ContainerElementDesc containerElementDesc in _containerElementDescs)
			{
				if(containerElementDesc.ElementType == elementType)
				{
					return containerElementDesc;
				}
			}
			return null;
		}

		//Function to read files
		private void ConstructDatabase()
		{
			string[] names = Enum.GetNames(typeof(BookType));
			for (int i = 0; i < names.Length; i++)
			{
				ReadFileDatabase(names[i], (BookType)i);
			}
		}

		private void ReadFileDatabase(string bookName, BookType bookType)
		{
			string bookFolderPath = _databaseRep + "/" + bookName + "/";
			string[] namesElementType = Enum.GetNames(typeof(ElementType));
			if (System.IO.Directory.Exists(bookFolderPath))
			{
				for (int i = 0; i < namesElementType.Length; i++)
				{
					string filePath = bookFolderPath + namesElementType[i] + ".csv";
					if (System.IO.File.Exists(filePath))
					{
						string[] allLines = System.IO.File.ReadAllLines(filePath, Encoding.Default);
						int curLineIndex = 0;
						object elementSubType = null;
						for(int j=curLineIndex; j<allLines.Length; j++)
						{ 
							ReadLine(allLines[j], bookType, (ElementType)i, ref elementSubType);
						}
					}
				}
			}
		}

		private void ReadLine(string line, BookType bookType, ElementType elementType, ref object elementSubType)
		{
			if(string.IsNullOrEmpty(line) == false)
			{
				string[] allLineElements = line.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
				if(allLineElements.Length > 0)
				{
					if(allLineElements.Length > 1)
					{
						ContainerElementDesc containerElementDesc = GetContainerElementDesc(elementType);
						if(containerElementDesc != null)
						{
							containerElementDesc.ReadLine(allLineElements, bookType, elementType, elementSubType);
						}
					}
					else
					{
						Type subType = TypeHelper.GetSubType(elementType);
						if(subType != null && allLineElements[0] != "-" && allLineElements[0] != "-")
						{
							elementSubType = RetrieveSubType(subType, allLineElements[0]);
						}
					}
				}
			}
		}

		private bool RetrieveType(ref ElementType elementType, string line)
		{
			string[] names = Enum.GetNames(typeof(ElementType));
			for(int i=0; i<names.Length; i++)
			{
				if(names[i] == line)
				{
					elementType = (ElementType)i;
					return true;
				}
			}
			return false;
		}

		private object RetrieveSubType(Type subType, string line)
		{
			try
			{
				return Enum.Parse(subType, line);
			}

			catch
			{
				return null;
			}
		}
	}
}
