using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class ElementDescAttachment : ElementDesc
	{
		#region Enums
		
		#endregion Enums

		#region Fields
		
		#endregion Fields

		#region Accessors
		#endregion Accessors

		#region Methods
		#region Publics
		public ElementDescAttachment()
		{
			_listProperty.Clear();
			_listProperty.Add(new PropertyEnum<ElementType>(PropertyType.ElementType, TypeHelper.GetPropertyTypeNbChar(PropertyType.ElementType)));
			_listProperty.Add(new PropertyEnum<Enum>(PropertyType.ElementSubType, TypeHelper.GetPropertyTypeNbChar(PropertyType.ElementSubType)));
			_listProperty.Add(new PropertyString(PropertyType.Book, TypeHelper.GetPropertyTypeNbChar(PropertyType.Book), false));
			_listProperty.Add(new PropertyString(PropertyType.Page, TypeHelper.GetPropertyTypeNbChar(PropertyType.Page), false));
			_listProperty.Add(new PropertyString(PropertyType.Name, TypeHelper.GetPropertyTypeNbChar(PropertyType.Name)));
			_listProperty.Add(new PropertyString(PropertyType.Illegality, TypeHelper.GetPropertyTypeNbChar(PropertyType.Illegality)));
			_listProperty.Add(new PropertyInt(PropertyType.Price, TypeHelper.GetPropertyTypeNbChar(PropertyType.Price)));
			_listProperty.Add(new PropertyInt(PropertyType.Encumbrance, TypeHelper.GetPropertyTypeNbChar(PropertyType.Encumbrance)));
			_listProperty.Add(new PropertyInt(PropertyType.HardPointRequired, TypeHelper.GetPropertyTypeNbChar(PropertyType.HardPointRequired)));
			_listProperty.Add(new PropertyInt(PropertyType.Rarity, TypeHelper.GetPropertyTypeNbChar(PropertyType.Rarity)));
			_listProperty.Add(new PropertyString(PropertyType.Description, TypeHelper.GetPropertyTypeNbChar(PropertyType.Description)));
		}

		#endregion Publics

		#region Internals
		#endregion Internals
		#endregion Methods
	}
}
