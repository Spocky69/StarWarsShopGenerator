using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class Criteria
	{
		public enum ComparaisonType
		{
			Different,
			Equal,
			Superior,
			Inferior
		}

		protected PropertyType _propertyType;
		protected ComparaisonType _comparaisonType;
		protected string _refValue;
		protected ElementType _elementType;

		public Criteria(ElementType elementType, PropertyType propertyType, ComparaisonType comparaisonType, string refValue)
		{
			_propertyType = propertyType;
			_comparaisonType = comparaisonType;
			_refValue = refValue;
			_elementType = elementType;
		}

		public virtual bool IsValid(ElementDesc elementDesc)
		{
			int compareValue = elementDesc.GetProperty(_propertyType).CompareTo(_refValue);
			switch (_comparaisonType)
			{
				case ComparaisonType.Equal: return compareValue == 0;
				case ComparaisonType.Different: return compareValue != 0;
				case ComparaisonType.Superior: return compareValue <= 0;
				case ComparaisonType.Inferior: return compareValue >= 0;
				default: return false;
			}
		}

		static public List<ElementDesc> FilterList(List<ElementDesc> elementDescs, List<Criteria> listCriterias)
		{
			if (elementDescs != null)
			{
				List<ElementDesc> elementDescsFiltered = elementDescs.FindAll((ElementDesc element) => IsElementValid(element, listCriterias));
				return elementDescsFiltered;
			}
			return null;
		}

		static private bool IsElementValid(ElementDesc element, List<Criteria> listCriterias)
		{
			foreach (Criteria criteria in listCriterias)
			{
				if (criteria._elementType.ToString() == element.GetPropertyValue(PropertyType.ElementType) || criteria._elementType == ElementType.Invalid)
				{
					if (criteria.IsValid(element) == false)
					{
						return false;
					}
				}
			}
			return true;
		}

		static private List<Criteria> FilterListCriteriaFromElementType(ElementDesc element, List<Criteria> listCriterias)
		{
			List<Criteria> listCriteriaFiltered = new List<Criteria>();

			//Foreach property get the good criteria
			foreach (PropertyType propertyType in Enum.GetValues(typeof(PropertyType)))
			{
				foreach (ComparaisonType comparaisonType in Enum.GetValues(typeof(Criteria.ComparaisonType)))
				{
					Criteria chosenCriteria = null;
					foreach (Criteria criteria in listCriterias)
					{
						if (criteria._propertyType == propertyType && criteria._comparaisonType == comparaisonType)
						{
							if (criteria._elementType.ToString() == element.GetPropertyValue(PropertyType.ElementType))
							{
								chosenCriteria = criteria;
								break;
							}
							else if (criteria._elementType == ElementType.Invalid)
							{
								chosenCriteria = criteria;
							}
						}
					}
					if (chosenCriteria != null)
					{
						listCriteriaFiltered.Add(chosenCriteria);
					}
				}
			}
			return listCriteriaFiltered;
		}
	}
}
