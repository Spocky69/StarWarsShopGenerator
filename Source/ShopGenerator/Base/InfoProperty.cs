using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public 	class InfoProperty
	{
		private string _name;
		private PropertyType _propertyType;

		public string Name { get { return _name; } }
		public PropertyType PropertyType { get { return _propertyType; } }

		public InfoProperty(string name, PropertyType propertyType)
		{
			_name = name;
			_propertyType = propertyType;
		}
	}
}
