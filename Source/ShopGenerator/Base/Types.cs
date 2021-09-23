using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public static class TypeHelper
	{
		public static Type GetSubType(ElementType elementType)
		{
			switch(elementType)
			{
				case ElementType.Gear: return typeof(ElementSubTypeGear);
				case ElementType.Weapon: return typeof(ElementSubTypeWeapon);
				case ElementType.Armor: return null;
				case ElementType.Attachment: return typeof(ElementSubTypeAttachment);
				case ElementType.BlackMarket: return typeof(ElementSubTypeBlackMarket);
				default: return null;
			}
		}

		public static int GetPropertyTypeNbChar(PropertyType propertyType)
		{
			switch(propertyType)
			{
				case PropertyType.ElementType:
				case PropertyType.ElementSubType: return -1;
				case PropertyType.Name: return 39;
				case PropertyType.Price: return 8;
				case PropertyType.Book: return 3;
				case PropertyType.Special: return 45;
				case PropertyType.Page: return 3;
				case PropertyType.Skill: return 4;
				case PropertyType.Range: return 3;
				case PropertyType.Description: return 70;
				default: return Localization.Instance.GetValue(typeof(PropertyType), propertyType).Length;
			}
		}
	}

	public enum PropertyType
	{
		Invalid,
		ElementType,
		ElementSubType,
		Book,
		Page,
		Name,
		Price,
		Encumbrance,
		Rarity,
		Skill,
		Damage,
		Range,
		Critic,
		HardPoint,
		Special,
		Defense,
		Soak,
		HardPointRequired,
		Illegality,
		Description
	}

	public enum BookType
	{
		AgeOfRebellion,
		CyphersAndMasks,
		DangerousCovenant,
		DesperateAllies,
		DisciplesOfHarmony,
		EdgeOfEmpire,
		EndlessVigil,
		EnterTheUnknown,
		FarHorizons,
		FlyCasual,
		ForceAndDestiny,
		ForgedInBattle,
		KeepingThePeace,
		KnightsOfFate,
		LeadByExample,
		LordsOfNalHutta,
		NexusOfPower,
		NoDisintegrations,
		SavageSpirits,
		SpecialModifications,
		StayOnTarget,
		StrongholdsOfResistance,
		SunsOfFortune,
		UnlimitedPower
	}

	public enum ElementType
	{
		Invalid=-1,
		Gear,
		Weapon,
		Armor,
		Attachment,
		BlackMarket
	}

	public enum ElementSubTypeGear
	{
		Communications,
		Poisons,
		Cybernetic,
		Detection,
		Medical,
		Security,
		Survival,
		Tools,
		Recreational,
		Droids
	}

	public enum ElementSubTypeWeapon
	{
		Energy,
		Slugthrowers,
		Thrown,
		Others,
		Brawling,
		Melee,
		Lightsaber,
		Tools
	}

	public enum ElementSubTypeAttachment
	{
		Weapon,
		Armor,
		Tool,
		Vehicle,
		Crystal
	}

	public enum ElementSubTypeBlackMarket
	{
		Drogs,
		Illegal
	}

	public enum Illegality
	{
		Legal,
		NotLegal,
		Both
	}
}	
