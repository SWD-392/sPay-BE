using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.Enum
{
	public enum OrderStatusEnum
	{
		[Description("Thành công")]
		Succeeded = 1,

		[Description("Thất bại")]
		Failed = 2,

		[Description("Đã bị xoá")]
		Deleted = 3,
	}
}
