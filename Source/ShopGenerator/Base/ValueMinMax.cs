using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator.Base
{
	[Serializable]
	public class ValueMinMax
	{
		private int _min;
		private int _max;

		public ValueMinMax()
		{

		}

		public ValueMinMax(int min, int max) 
		{ 
			_min = min;
			_max = max;
		}

		public int Min
		{
            get { return _min; }
			set
            {
                if(_min != value)
                {
                    _min = value;
                    MainWindow.Instance.OnPropertyChanged();
				}
            }
        }

		public int Max
		{
			get { return _max; }
			set
			{
				if (_max != value)
				{
					_max = value;
					MainWindow.Instance.OnPropertyChanged();
				}
			}
		}

		public int Random
		{
			get { return Service.Random_Range(_min, _max); }
		}

	}
}
