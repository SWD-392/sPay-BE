using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.Enum
{
	public enum BasicStatusEnum
	{
		[Description("Còn sử dụng")]
		Available = 1,

		[Description("Đã bị xoá")]
		Deleted = 2,
	}
}
