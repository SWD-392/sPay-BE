using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.Enum
{
	public enum AccountStatusEnum
	{
		[Description("Đang hoạt động")]
		Active = 1,

		[Description("Bị Khoá")]
		Banned = 2,

		[Description("Đã bị xoá")]
		Deleted = 3,
	}
}
