using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class Localization : Singleton<Localization>
	{
		public class EnumLocalization
		{
			private Type _enumTypeToLocalize;
			private List<EnumValueLocalization> _listEnumValueLocalization = new List<EnumValueLocalization>();

			public Type EnumTypeToLocalize { get { return _enumTypeToLocalize; } }

			public EnumLocalization(Type enumTypeToLocalize)
			{
				_enumTypeToLocalize = enumTypeToLocalize;
			}

			public void Init(string[] lines)
			{
				for (int i = 0; i < lines.Length; i++)
				{
					if (string.IsNullOrEmpty(lines[i]) == false)
					{
						string[] values = lines[i].Split(';');
						object enumValue = Enum.Parse(_enumTypeToLocalize, values[0]);
						AddEnumValueLocalization(enumValue, values[1]);
					}
				}
			}

			public void AddEnumValueLocalization(object enumValue, string value)
			{
				EnumValueLocalization enumValueLocalization = new EnumValueLocalization(enumValue, value);
				_listEnumValueLocalization.Add(enumValueLocalization);
			}

			public string GetEnumValueLocalization(object enumValue)
			{
				foreach (EnumValueLocalization enumValueLocalization in _listEnumValueLocalization)
				{
					if ((int)enumValueLocalization.EnumValue == (int)enumValue)
					{
						return enumValueLocalization.Value;
					}
				}
				return enumValue.ToString();
			}
		}

		public class EnumValueLocalization
		{
			private object _enumValue;
			private string _value;

			public object EnumValue { get { return _enumValue; } }
			public string Value { get { return _value; } }

			public EnumValueLocalization(object enumValue, string value)
			{
				_enumValue = enumValue;
				_value = value;
			}
		}

		private List<EnumLocalization> _enumLocalizations = new List<EnumLocalization>();

		public Localization()
		{
			_instance = this;
		}

		public void Init(string databaseRep)
		{
			string localizationRep = databaseRep + "/Localization/";

			_enumLocalizations.Add(new EnumLocalization(typeof(BookType)));
			_enumLocalizations.Add(new EnumLocalization(typeof(ElementType)));
			_enumLocalizations.Add(new EnumLocalization(typeof(ElementSubTypeGear)));
			_enumLocalizations.Add(new EnumLocalization(typeof(ElementSubTypeWeapon)));
			_enumLocalizations.Add(new EnumLocalization(typeof(ElementSubTypeAttachment)));
			_enumLocalizations.Add(new EnumLocalization(typeof(ElementSubTypeBlackMarket)));
			_enumLocalizations.Add(new EnumLocalization(typeof(PropertyType)));
			foreach (EnumLocalization enumLocalization in _enumLocalizations)
			{
				string filePath = localizationRep + enumLocalization.EnumTypeToLocalize.ToString() + ".loc";
				enumLocalization.Init(File.ReadAllLines(filePath));
			}
		}

		public string GetValue(Type type, object enumValue)
		{
			foreach (EnumLocalization enumLocalization in _enumLocalizations)
			{
				if (enumLocalization.EnumTypeToLocalize == type)
				{
					return enumLocalization.GetEnumValueLocalization(enumValue);
				}
			}
			return enumValue.ToString();
		}
	}
}
