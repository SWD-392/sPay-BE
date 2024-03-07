using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.Enum
{
	public enum RoleEnum
	{
		[Description("Admin")]
		Admin = 1,

		[Description("Khách hàng")]
		Customer = 2,

		[Description("Cửa hàng")]
		Store = 3,
	}
}
