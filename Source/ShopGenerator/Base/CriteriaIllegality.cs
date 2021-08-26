using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class CriteriaIllegality : Criteria
	{
		private Illegality _illegality;

		public CriteriaIllegality(ElementType elementType, Illegality illegality ) : base(elementType, PropertyType.Illegality, ComparaisonType.Equal, "")
		{
			_illegality = illegality;
		}

		public override bool IsValid(ElementDesc elementDesc)
		{
			string propertyValue = elementDesc.GetPropertyValue(_propertyType);

			switch(_illegality)
			{
				case Illegality.NotLegal: 
				{
					if(propertyValue == "I")
					{
						return true;
					}
				}
				break;

				case Illegality.Legal: 
				{
					if(propertyValue != "I")
					{
						return true;
					}
				}
				break;

				case Illegality.Both:
				{
					return true;
				}
			}
			return false;
		}
	}
}
