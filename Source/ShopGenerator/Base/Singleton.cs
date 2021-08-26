using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class Singleton<T>
	{
		static protected T _instance;
		static public T Instance { get { return _instance; } }
	}
}
