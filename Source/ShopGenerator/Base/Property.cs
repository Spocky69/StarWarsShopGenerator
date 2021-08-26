using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public abstract class IProperty
	{
		private PropertyType _elementType;
		private int _nbMaxChar;
		private bool _tab;

		public IProperty(PropertyType elementType, int nbMaxChar, bool tab=true)
		{
			_elementType = elementType;
			_nbMaxChar = nbMaxChar;
			_tab = tab;
		}

		public PropertyType ElementType { get { return _elementType; } }
		public int NbMaxChar { get { return _nbMaxChar; } }
		public bool Tab { get { return _tab; } }

		public abstract void SetValue(object value);
		public abstract override string ToString();
		public abstract object GetValue();
		public abstract Type GetValueType();
		public abstract int CompareTo(string value);
	}

	public class PropertyString : IProperty
	{
		private string _value;

		string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		public PropertyString(PropertyType elementType, int nbTabs, bool tab=true) : base(elementType, nbTabs, tab) { }
		public override void SetValue(object value)
		{
			_value = (string)value;
		}

		public override string ToString()
		{
			return _value;
		}

		public override object GetValue()
		{
			return _value;
		}

		public override Type GetValueType()
		{
			return typeof(string);
		}

		public override int CompareTo(string value)
		{
			return string.Compare(_value, value, true);
		}
	}

	public class PropertyInt : IProperty
	{
		private int _value;

		int Value
		{ 
			get { return _value; }
			set { _value = value; }
		}

		public PropertyInt(PropertyType elementType, int nbTabs, bool tab=true) : base(elementType, nbTabs, tab) { }

		public override void SetValue(object value)
		{
			if(value.GetType() == typeof(string))
			{
				_value = int.Parse((string)value);
			}
			else
			{
				_value = (int)value;
			}
		}

		public override string ToString()
		{
			return _value.ToString();
		}

		public override object GetValue()
		{
			return _value;
		}

		public override Type GetValueType()
		{
			return typeof(int);
		}

		public override int CompareTo(string value)
		{
			int valueToCompare = int.Parse(value);
			return valueToCompare - _value;
		}
	}

	public class PropertyEnum<T> : IProperty where T : Enum
	{
		private T _value;

		T Value
		{ 
			get { return _value; }
			set { _value = value; }
		}

		public PropertyEnum(PropertyType elementType, int nbTabs, bool tab=true ) : base(elementType, nbTabs, tab) { }

		public override void SetValue(object value)
		{
			_value = (T)value;
		}

		public override string ToString()
		{
			return _value.ToString();
		}

		public override object GetValue()
		{
			return _value;
		}

		public override Type GetValueType()
		{
			return _value.GetType();
		}

		public override int CompareTo(string value)
		{
			return string.Compare(ToString(), value, true);
		}
	}
}
