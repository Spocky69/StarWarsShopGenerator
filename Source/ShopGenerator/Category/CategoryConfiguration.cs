using ShopGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	[Serializable]
	public abstract class  CategoryConfiguration
	{
		protected ElementType _elementType;
		protected ValueMinMax _number = new ValueMinMax(5, 15);
		protected ValueMinMax _rarity = new ValueMinMax(1, 10);
		protected Illegality _illegal;
		
		public ElementType ElementType { get { return _elementType; } set { _elementType = value; } }
		public ValueMinMax Number { get { return _number; } set { _number = value; } }
		public ValueMinMax Rarity { get { return _rarity; } set { _rarity = value; } }
		public Illegality Illegal
        {
            get { return _illegal; }
            set
            {
                if(_illegal != value)
                {
                    _illegal = value;
                    MainWindow.Instance.OnPropertyChanged();
                }
            }
        }

		public CategoryConfiguration()
		{

		}

		public CategoryConfiguration(ElementType elementType)
		{
			_elementType = elementType;
		}

		public abstract List<LimitationNumber> CreateLimitationNumberList();
	}
}
