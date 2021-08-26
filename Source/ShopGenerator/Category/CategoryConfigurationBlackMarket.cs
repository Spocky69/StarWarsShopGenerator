using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class CategoryConfigurationBlackMarket : CategoryConfiguration
	{
		private ValueMinMax _drogs = new ValueMinMax(5, 10);
		private ValueMinMax _illegals = new ValueMinMax(5, 10);

		public ValueMinMax Drogs { get { return _drogs; } set { _drogs = value; } }
		public ValueMinMax Illegals { get { return _illegals; } set { _illegals = value; }  }

		public CategoryConfigurationBlackMarket()
		{

		}

		public CategoryConfigurationBlackMarket(ElementType elementType) : base(elementType)
		{
			
		}

		public override List<LimitationNumber> CreateLimitationNumberList()
		{
			List<LimitationNumber> listLimitationNumber = new List<LimitationNumber>();
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementType), ElementType.BlackMarket, Number.Min, Number.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeBlackMarket), ElementSubTypeBlackMarket.Illegal, Illegals.Min, Illegals.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeBlackMarket), ElementSubTypeBlackMarket.Drogs, Drogs.Min, Drogs.Max));
			return listLimitationNumber;
		}
	}
}
