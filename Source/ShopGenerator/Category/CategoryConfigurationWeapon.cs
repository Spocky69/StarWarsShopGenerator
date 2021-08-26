using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class CategoryConfigurationWeapon : CategoryConfiguration
	{
		private ValueMinMax _energy = new ValueMinMax(5, 10);
		private ValueMinMax _slugthrowers = new ValueMinMax(2, 4);
		private ValueMinMax _thrown = new ValueMinMax(1, 3);
		private ValueMinMax _others = new ValueMinMax(2, 4);
		private ValueMinMax _brawling = new ValueMinMax(1, 3);
		private ValueMinMax _melee = new ValueMinMax(5, 10);
		private ValueMinMax _lightsaber = new ValueMinMax(1, 3);

		public ValueMinMax Energy { get { return _energy; } set { _energy = value; } }
		public ValueMinMax Slugthrowers { get { return _slugthrowers; } set { _slugthrowers = value; } }
		public ValueMinMax Thrown { get { return _thrown; } set { _thrown = value; } }
		public ValueMinMax Others{ get { return _others; } set { _others = value; } }
		public ValueMinMax Brawling { get { return _brawling; } set { _brawling = value; } }
		public ValueMinMax Melee { get { return _melee; } set { _melee = value; } }
		public ValueMinMax Lightsaber { get { return _lightsaber; } set { _lightsaber = value; } }

		public CategoryConfigurationWeapon()
		{

		}

		public CategoryConfigurationWeapon(ElementType elementType) : base(elementType)
		{
			_number = new ValueMinMax(10, 30);
		}

		public override List<LimitationNumber> CreateLimitationNumberList()
		{
			List<LimitationNumber> listLimitationNumber = new List<LimitationNumber>();
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementType), ElementType.Weapon, Number.Min, Number.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeWeapon), ElementSubTypeWeapon.Energy, Energy.Min, Energy.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeWeapon), ElementSubTypeWeapon.Slugthrowers, Slugthrowers.Min, Slugthrowers.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeWeapon), ElementSubTypeWeapon.Thrown, Thrown.Min, Thrown.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeWeapon), ElementSubTypeWeapon.Others, Others.Min, Others.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeWeapon), ElementSubTypeWeapon.Brawling, Brawling.Min, Brawling.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeWeapon), ElementSubTypeWeapon.Melee, Melee.Min, Melee.Max));
			listLimitationNumber.Add(new LimitationNumber(typeof(ElementSubTypeWeapon), ElementSubTypeWeapon.Lightsaber, Lightsaber.Min, Lightsaber.Max));
			return listLimitationNumber;
		}
	}
}
