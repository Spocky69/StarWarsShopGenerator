using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class CategoryConfigurationArmor : CategoryConfiguration
	{
		public CategoryConfigurationArmor()
		{

		}

		public CategoryConfigurationArmor(ElementType elementType)  : base(elementType)
		{
			_number = new ValueMinMax(10, 20);
		}

		public override List<LimitationNumber> CreateLimitationNumberList()
		{
			List<LimitationNumber> listLimitationNumber = new List<LimitationNumber>();
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementType), ElementType.Armor, Number.Min, Number.Max));
			return listLimitationNumber;
		}
	}
}
