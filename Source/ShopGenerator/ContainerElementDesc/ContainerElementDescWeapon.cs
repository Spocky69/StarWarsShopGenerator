using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopGenerator
{
	public class ContainerElementDescWeapon : ContainerElementDesc
	{
		public ContainerElementDescWeapon(ElementType elementType) : base(elementType)
		{
		}

		public override ElementDesc CreateElementDesc()
		{
			return new ElementDescWeapon();
		}
	}
}
