using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class CriteriaStringContains : Criteria
	{
		public CriteriaStringContains(ElementType elementType, PropertyType propertyType, ComparaisonType comparaisonType, string refValue) : base(elementType, propertyType, comparaisonType, refValue)
		{

		}

		public override bool IsValid(ElementDesc elementDesc)
		{
			if(elementDesc.GetPropertyValue(_propertyType).IndexOf(_refValue, 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
			{
				return true;
			}
			return false;
		}
	}
}
