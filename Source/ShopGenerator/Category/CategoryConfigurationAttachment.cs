using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class CategoryConfigurationAttachment : CategoryConfiguration
	{
		private ValueMinMax _weapon = new ValueMinMax(5, 10);
		private ValueMinMax _armor = new ValueMinMax(4, 7);
		private ValueMinMax _tool = new ValueMinMax(0, 0);
		private ValueMinMax _vehicle = new ValueMinMax(0, 0);
		private ValueMinMax _crystal = new ValueMinMax(0, 0);


		public ValueMinMax Weapon { get { return _weapon; } set { _weapon = value; } }
		public ValueMinMax Armor { get { return _armor; } set { _armor = value; } }
		public ValueMinMax Tool { get { return _tool; } set { _tool = value; } }
		public ValueMinMax Vehicle { get { return _vehicle; } set { _vehicle = value; } }
		public ValueMinMax Crystal { get { return _crystal; } set { _crystal = value; } }

		public CategoryConfigurationAttachment()
		{

		}

		public CategoryConfigurationAttachment(ElementType elementType)  : base(elementType)
		{
			_number = new ValueMinMax(10, 20);
		}

		public override List<LimitationNumber> CreateLimitationNumberList()
		{
			List<LimitationNumber> listLimitationNumber = new List<LimitationNumber>();
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementType), ElementType.Attachment, Number.Min, Number.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeAttachment), ElementSubTypeAttachment.Weapon, Weapon.Min, Weapon.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeAttachment), ElementSubTypeAttachment.Armor, Armor.Min, Armor.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeAttachment), ElementSubTypeAttachment.Tool, Tool.Min, Tool.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeAttachment), ElementSubTypeAttachment.Crystal, Crystal.Min, Crystal.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeAttachment), ElementSubTypeAttachment.Vehicle, Vehicle.Min, Vehicle.Max));
			return listLimitationNumber;
		}
	}
}
