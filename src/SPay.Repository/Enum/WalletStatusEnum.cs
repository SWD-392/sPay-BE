using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.Enum
{
	public enum WalletStatusEnum
	{
		[Description("Hoạt động")]
		Active = 1,

		[Description("Hết hạn")]
		Expired = 2,
	}
}
