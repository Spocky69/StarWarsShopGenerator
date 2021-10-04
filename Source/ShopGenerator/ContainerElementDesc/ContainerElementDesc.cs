using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class ContainerElementDesc
	{
		protected List<InfoProperty> _descProperties = new List<InfoProperty>();
		protected List<ElementDesc> _elementDescs = new List<ElementDesc>();
		protected ElementType _elementType;

		public ElementType ElementType { get { return _elementType; } }
		public List<ElementDesc> ElementDescs { get { return _elementDescs; } }

		#region methods
		#region constrcutor
		public ContainerElementDesc(ElementType elementType)
		{
			_elementType = elementType;
		}
		#endregion constrcutor

		#region publics
		public virtual ElementDesc CreateElementDesc()
		{
			return new ElementDesc();
		}

		public void ReadLine(string[] lineElements, string bookType, ElementType elementType, object elementSubType)
		{
			ElementDesc elementDesc = CreateElementDesc();
			int index = 0;
			elementDesc.FillFromDatabaseString(ref index, lineElements, bookType, elementType, (Enum)elementSubType);
			_elementDescs.Add(elementDesc);
		}

		public string GenerateLines(List<ElementDesc> listElementsWithFilter)
		{
			//Filter for this Container
			Criteria criteria = new Criteria(ElementType.Invalid, PropertyType.ElementType, Criteria.ComparaisonType.Equal, _elementType.ToString());
			List<Criteria> criterias = new List<Criteria>();
			criterias.Add(criteria);
			return GenerateLinesWithElementDescList(Criteria.FilterList(listElementsWithFilter, criterias));
		}

		public List<ElementDesc> GenerateListWithCriteriasAndLimitationNumber(List<Criteria> listCriterias, List<LimitationNumber> listLimitationNumber)
		{
			List<ElementDesc> finalList = FilterListByCriterion(_elementDescs, listCriterias);

			//Filter by subtype number
			Type subType = TypeHelper.GetSubType(ElementType);
			if (subType != null)
			{
				List<ElementDesc> filterListByNumber = new List<ElementDesc>();
				string[] subTypeNames = Enum.GetNames(subType);
				foreach (int i in Enum.GetValues(subType))
				{
					Criteria criteria = new Criteria(ElementType.Invalid, PropertyType.ElementSubType, Criteria.ComparaisonType.Equal, subTypeNames[i]);
					List<Criteria> criterias = new List<Criteria>();
					criterias.Add(criteria);
					List<ElementDesc> listToGenerate = FilterListByCriterion(finalList, criterias);
					List<ElementDesc> listElementToAdd = FilterListByLimitationNumber(subType, i, PropertyType.ElementSubType, listToGenerate, listLimitationNumber);
					if(listElementToAdd != null)
					{
						filterListByNumber.AddRange(listElementToAdd);
					}
				}
				finalList = filterListByNumber;
			}

			//Filter by type number
			finalList = FilterListByLimitationNumber(typeof(ElementType), _elementType, PropertyType.ElementType, finalList, listLimitationNumber);
			return finalList;
		}

		#endregion publics

		#region private
		private List<ElementDesc> FilterListByCriterion(List<ElementDesc> elementDescs, List<Criteria> listCriterias)
		{
			return Criteria.FilterList(elementDescs, listCriterias);
		}

		private List<ElementDesc> FilterListByLimitationNumber(Type type, object elementTypeValue, PropertyType propertyType, List<ElementDesc> elementDescs, List<LimitationNumber> listLimitationNumber)
		{
			LimitationNumber limitationNumber = LimitationNumber.GetLimitationNumberByType(type, elementTypeValue, listLimitationNumber);
			if(limitationNumber != null)
			{
				return limitationNumber.FilterElementDesc(elementDescs, propertyType);
			}
			else
			{
				return elementDescs;
			}
		}

		private string GenerateLinesWithElementDescList(List<ElementDesc> elementDescs)
		{	
			if(elementDescs.Count > 0)
			{
				Type subType = TypeHelper.GetSubType(ElementType);
				
				//Compute the title
				string lines = ComputeTitle(140);
				lines += elementDescs[0].ToTitleString();
				lines += "\n";

				if(subType != null)
				{
					string finalString = "";
					string[] enumNames = Enum.GetNames(subType);
					for(int i=0; i<enumNames.Length; i++)
					{
						Criteria criteria = new Criteria(ElementType.Invalid, PropertyType.ElementSubType, Criteria.ComparaisonType.Equal, enumNames[i]);
						List<Criteria> criterias = new List<Criteria>();
						criterias.Add(criteria);

						List<ElementDesc> listToGenerate = FilterListByCriterion(elementDescs, criterias);

						finalString += GenerateLinesSubType(listToGenerate);
					}
					lines += finalString;
				}
				else
				{
					lines += GenerateLinesSubType(elementDescs);
				}
				lines += "\n";
				return lines;
			}
			return "";
		}

		private string ComputeTitle(int nbCharInLines)
		{
			string titleString = Localization.Instance.GetValue(typeof(ElementType), _elementType);

			string elementTypeString = "";
			elementTypeString = elementTypeString.PadLeft(12, '-');
			elementTypeString += " " + titleString + " ";
			elementTypeString = elementTypeString.PadRight(nbCharInLines+2, '-');
			string finalString=elementTypeString;
			finalString+="\n";
			return finalString;
		}

		//
		private string GenerateLinesSubType(List<ElementDesc> elementDescsToFilter)
		{
			List<ElementDesc> listToGenerate = elementDescsToFilter;
			
			if(listToGenerate.Count > 0)
			{
				string finalString = "";
				string curSubTypeName = ""; 
				int nbCharInLines = elementDescsToFilter[0].GetNbCharacter();

				foreach(ElementDesc elementDesc in listToGenerate)
				{
					string nextSubTypeName = "";
					IProperty propertySubType = elementDesc.GetProperty(PropertyType.ElementSubType);
					if(propertySubType != null)
					{
						nextSubTypeName = Localization.Instance.GetValue(propertySubType.GetValueType(), propertySubType.GetValue());
					}
					
					if(string.IsNullOrEmpty(nextSubTypeName) == false && nextSubTypeName != curSubTypeName)
					{
						int nbCharInLineSubString = (nbCharInLines / 2);
						string subTypeString = "";
						subTypeString = subTypeString.PadLeft(4, '-');
						subTypeString += " " + nextSubTypeName + " ";
						subTypeString = subTypeString.PadRight(nbCharInLineSubString, '-');
						finalString += subTypeString;
						finalString += "\n";
						curSubTypeName = nextSubTypeName;
					}
					finalString += elementDesc.ToDatabaseString();
					finalString += "\n";
				}
				finalString += "\n";
				return finalString;
			}
			return "";
		}
		#endregion privates
		#endregion methods
	}
}
