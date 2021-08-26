using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class CriteriasForSubType
	{
		private List<Criteria> _listCriteria = new List<Criteria>();
		protected ElementType _elementType;

		public ElementType ElementType { get { return _elementType; } }
		public List<Criteria> ListCriteria { get { return _listCriteria; } }

		public CriteriasForSubType(ElementType elementType, List<Criteria> listCriteria)
		{
			_elementType = elementType;
			_listCriteria = listCriteria;
		}
	}
}
