using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class ElementDesc
	{
		#region Enums
		
		#endregion Enums

		#region Fields
		protected List<IProperty> _listProperty = new List<IProperty>();
		#endregion Fields

		#region Accessors
		#endregion Accessors

		#region Methods
		#region Publics
		public ElementDesc()
		{
			_listProperty.Add(new PropertyEnum<ElementType>(PropertyType.ElementType, TypeHelper.GetPropertyTypeNbChar(PropertyType.ElementType)));
			_listProperty.Add(new PropertyEnum<Enum>(PropertyType.ElementSubType, TypeHelper.GetPropertyTypeNbChar(PropertyType.ElementSubType)));
			_listProperty.Add(new PropertyString(PropertyType.Book, TypeHelper.GetPropertyTypeNbChar(PropertyType.Book), false));
			_listProperty.Add(new PropertyString(PropertyType.Page, TypeHelper.GetPropertyTypeNbChar(PropertyType.Page), false));
			_listProperty.Add(new PropertyString(PropertyType.Name, TypeHelper.GetPropertyTypeNbChar(PropertyType.Name)));
			_listProperty.Add(new PropertyString(PropertyType.Illegality, TypeHelper.GetPropertyTypeNbChar(PropertyType.Illegality), false));
			_listProperty.Add(new PropertyInt(PropertyType.Price, TypeHelper.GetPropertyTypeNbChar(PropertyType.Price)));
			_listProperty.Add(new PropertyInt(PropertyType.Encumbrance, TypeHelper.GetPropertyTypeNbChar(PropertyType.Encumbrance), false));
			_listProperty.Add(new PropertyInt(PropertyType.Rarity, TypeHelper.GetPropertyTypeNbChar(PropertyType.Rarity), false));
			_listProperty.Add(new PropertyString(PropertyType.Description, TypeHelper.GetPropertyTypeNbChar(PropertyType.Description)));
		}

		public virtual string ToDatabaseString()
		{
			string finalText = "";
			foreach(IProperty property in _listProperty)
			{ 
				AddElement(ref finalText, property);
			}
			return finalText;
		}

		public string ToTitleString()
		{
			string finalText = "";
			int nbTotalCharacters = 0;
			foreach(IProperty property in _listProperty)
			{
				if (property.NbMaxChar >= 0)
				{
					nbTotalCharacters += property.NbMaxChar;
					string propertyString = Localization.Instance.GetValue(typeof(PropertyType), property.ElementType);
					finalText += propertyString;
					finalText = finalText.PadRight(nbTotalCharacters, ' ');
					finalText += " ";
					nbTotalCharacters += 1;
				}
			}
			return finalText;
		}

		public virtual void FillFromDatabaseString(ref int curIndex , string[] arrayElems, string bookType, ElementType elementType, Enum subType)
		{
			int curPropertyIndex = 0;
			_listProperty[curPropertyIndex++].SetValue(elementType);
			if(subType != null)
			{
				_listProperty[curPropertyIndex++].SetValue(subType);
			}
			_listProperty[curPropertyIndex++].SetValue(bookType);

			for(int i=0; i<arrayElems.Length; i++)
			{ 
				_listProperty[i+curPropertyIndex].SetValue(arrayElems[i]);
			}
			curIndex += _listProperty.Count;
		}

		public string GetPropertyValue(PropertyType propertyType)
		{
			IProperty property = GetProperty(propertyType);
			if(property != null)
			{
				return property.ToString();
			}
			return "";
		}

		public int GetNbCharacter()
		{
			int nbCharacters = 0;
			foreach (IProperty property in _listProperty)
			{
				if(property.NbMaxChar > -1)
				{
					nbCharacters += property.NbMaxChar;
					if(property.Tab)
					{
						nbCharacters +=4;
					}
				}
			}
			return nbCharacters;
		}

		public IProperty GetProperty(PropertyType propertyType)
		{
			foreach(IProperty property in _listProperty)
			{
				if(property.ElementType == propertyType)
				{
					return property;
				}
			}
			return null;
		}
		#endregion Publics

		#region Internals
		private void AddElement(ref string total, IProperty property)
		{
			if(property.NbMaxChar >= 0 && property.GetValue() != null)
			{
				string propertyString = Localization.Instance.GetValue(property.GetValueType(), property.GetValue());
				if(propertyString != null && property.NbMaxChar > 0)
				{
					if(propertyString.Length > property.NbMaxChar)
					{
						propertyString = propertyString.Substring(0, property.NbMaxChar-3);
						propertyString += "...";
					}
					else
					{
						propertyString = propertyString.PadRight(property.NbMaxChar);
					}
				}
				total += propertyString;
				total += " ";
			}
		}

		
		#endregion Internals
		#endregion Methods
	}
}
