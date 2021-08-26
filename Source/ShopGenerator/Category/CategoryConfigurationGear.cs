using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class CategoryConfigurationGear : CategoryConfiguration
	{
		private ValueMinMax _communications = new ValueMinMax(5, 10);
		private ValueMinMax _poisons = new ValueMinMax(1, 3);
		private ValueMinMax _cybernetic = new ValueMinMax(1, 3);
		private ValueMinMax _detection = new ValueMinMax(2, 5);
		private ValueMinMax _medical = new ValueMinMax(2, 5);
		private ValueMinMax _security = new ValueMinMax(5, 10);
		private ValueMinMax _survival = new ValueMinMax(5, 10);
		private ValueMinMax _tools = new ValueMinMax(5, 10);
		private ValueMinMax _recreational = new ValueMinMax(2, 5);
		private ValueMinMax _droids = new ValueMinMax(2, 5);

		public ValueMinMax Communications { get { return _communications; } set { _communications = value; } }
		public ValueMinMax Poisons { get { return _poisons; } set { _poisons = value; } }
		public ValueMinMax Cybernetic { get { return _cybernetic; } set { _cybernetic = value; } }
		public ValueMinMax Detection { get { return _detection; } set { _detection = value; } }
		public ValueMinMax Medical { get { return _medical; } set { _medical = value; } }
		public ValueMinMax Security { get { return _security; } set { _security = value; } }
		public ValueMinMax Survival { get { return _survival; } set { _survival = value; } }
		public ValueMinMax Tools { get { return _tools; } set { _tools = value; } }
		public ValueMinMax Recreational { get { return _recreational; } set { _recreational = value; } }
		public ValueMinMax Droids { get { return _droids; } set { _droids = value; } }

		public CategoryConfigurationGear()
		{

		}

		public CategoryConfigurationGear(ElementType elementType)  : base(elementType)
		{
			_number = new ValueMinMax(25, 50);
		}

		public override List<LimitationNumber> CreateLimitationNumberList()
		{
			List<LimitationNumber> listLimitationNumber = new List<LimitationNumber>();
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementType), ElementType.Gear, Number.Min, Number.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Communications, Communications.Min, Communications.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Poisons, Poisons.Min, Poisons.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Cybernetic, Cybernetic.Min, Cybernetic.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Detection, Detection.Min, Detection.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Medical, Medical.Min, Medical.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Security, Security.Min, Security.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Survival, Survival.Min, Survival.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Tools, Tools.Min, Tools.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Recreational, Recreational.Min, Recreational.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeGear), ElementSubTypeGear.Droids, Droids.Min, Droids.Max));

			return listLimitationNumber;
		}
	}
}
