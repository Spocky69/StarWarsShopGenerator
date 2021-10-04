using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class ElementDescWeapon : ElementDesc
	{
		#region Enums
		
		#endregion Enums

		#region Fields
		
		#endregion Fields

		#region Accessors
		#endregion Accessors

		#region Methods
		#region Publics
		public ElementDescWeapon()
		{
			_listProperty.Clear();
			_listProperty.Add(new PropertyEnum<ElementType>(PropertyType.ElementType, TypeHelper.GetPropertyTypeNbChar(PropertyType.ElementType)));
			_listProperty.Add(new PropertyEnum<Enum>(PropertyType.ElementSubType, TypeHelper.GetPropertyTypeNbChar(PropertyType.ElementSubType)));
			_listProperty.Add(new PropertyString(PropertyType.Book, TypeHelper.GetPropertyTypeNbChar(PropertyType.Book), false));
			_listProperty.Add(new PropertyString(PropertyType.Page, TypeHelper.GetPropertyTypeNbChar(PropertyType.Page), false));
			_listProperty.Add(new PropertyString(PropertyType.Name, TypeHelper.GetPropertyTypeNbChar(PropertyType.Name)));
			_listProperty.Add(new PropertyString(PropertyType.Skill, TypeHelper.GetPropertyTypeNbChar(PropertyType.Skill)));
			_listProperty.Add(new PropertyString(PropertyType.Damage, TypeHelper.GetPropertyTypeNbChar(PropertyType.Damage), false));
			_listProperty.Add(new PropertyInt(PropertyType.Critic, TypeHelper.GetPropertyTypeNbChar(PropertyType.Critic), false));
			_listProperty.Add(new PropertyString(PropertyType.Range, TypeHelper.GetPropertyTypeNbChar(PropertyType.Range)));
			_listProperty.Add(new PropertyInt(PropertyType.Encumbrance, TypeHelper.GetPropertyTypeNbChar(PropertyType.Encumbrance), false));
			_listProperty.Add(new PropertyInt(PropertyType.HardPoint, TypeHelper.GetPropertyTypeNbChar(PropertyType.HardPoint), false));
			_listProperty.Add(new PropertyString(PropertyType.Illegality, TypeHelper.GetPropertyTypeNbChar(PropertyType.Illegality), false));
			_listProperty.Add(new PropertyInt(PropertyType.Price, TypeHelper.GetPropertyTypeNbChar(PropertyType.Price)));
			_listProperty.Add(new PropertyInt(PropertyType.Rarity, TypeHelper.GetPropertyTypeNbChar(PropertyType.Rarity), false));
			_listProperty.Add(new PropertyString(PropertyType.Special, TypeHelper.GetPropertyTypeNbChar(PropertyType.Special)));
		}

		#endregion Publics

		#region Internals
		#endregion Internals
		#endregion Methods
	}
}
