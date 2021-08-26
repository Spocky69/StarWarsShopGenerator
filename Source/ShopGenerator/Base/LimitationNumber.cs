using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public interface ILimitationNumber
	{

	}

	public class LimitationNumber : ILimitationNumber
	{
		private int _minNumber;
		private int _maxNumber;
		private Type _elementType;
		private object _elementTypeValue;
		
		public Type GetElementType() { return _elementType; }
		public object GetElementTypeValue() { return _elementTypeValue; }

		public LimitationNumber(Type elementType, object elementTypeValue, int minNumber, int maxNumber)
		{
			_minNumber = minNumber;
			_maxNumber = maxNumber;
			_elementType = elementType;
			_elementTypeValue = elementTypeValue;
		}

		public List<ElementDesc> FilterElementDesc(List<ElementDesc> elementDescs, PropertyType propertyType)
		{
			if(elementDescs != null && elementDescs.Count > 0)
			{
				List<ElementDesc> listElementFiltered = elementDescs;
				if (propertyType != PropertyType.Invalid)
				{
					Criteria criteria = new Criteria(ElementType.Invalid, propertyType, Criteria.ComparaisonType.Equal, _elementTypeValue.ToString());
					List<Criteria> criterias = new List<Criteria>();
					criterias.Add(criteria);
					listElementFiltered = Criteria.FilterList(listElementFiltered, criterias);
				}

				if (_maxNumber >= 0 && _maxNumber >= _minNumber)
				{
					listElementFiltered.Random_ShuffleList();
					int nbElements = Service.Random_Range(_minNumber, _maxNumber);
					if(listElementFiltered.Count - nbElements > 0)
					{
						listElementFiltered.RemoveRange(nbElements, listElementFiltered.Count - nbElements);
					}
				}
				return listElementFiltered;
			}
			return null;
		}

		public static LimitationNumber GetLimitationNumberByType(Type elementType, object elementTypeValue, List<LimitationNumber> listLimitationNumber)
		{
			foreach(LimitationNumber limitationNumber in listLimitationNumber)
			{
				if(((elementTypeValue == null && limitationNumber.GetElementTypeValue() == null) ||
						(elementTypeValue != null && limitationNumber.GetElementTypeValue() != null && 
						(int)limitationNumber.GetElementTypeValue() == (int)elementTypeValue)) &&
					limitationNumber.GetElementType() == elementType)
				{
					return limitationNumber;
				}
			}
			return null;
		}
	}
}
