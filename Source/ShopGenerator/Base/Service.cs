using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator.Base
{
	public static class Service
	{
		static Random random = new Random();

		public static void Random_ShuffleList<T>(this IList<T> list)  
		{  
			int n = list.Count;  
			while (n > 1)
			{  
				n--;  
				int k = random.Next(0, n + 1);  
				T value = list[k];  
				list[k] = list[n];  
				list[n] = value;  
			}  
		}

		public static int Random_Range(int min, int max)
		{
			return random.Next(min, max);
		}
	}
}
