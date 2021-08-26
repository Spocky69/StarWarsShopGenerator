using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class ContainerElementDescArmor : ContainerElementDesc
	{
		public ContainerElementDescArmor(ElementType elementType) : base(elementType)
		{
		}

		public override ElementDesc CreateElementDesc()
		{
			return new ElementDescArmor();
		}
	}
}
