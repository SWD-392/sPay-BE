using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.Enum
{
	public enum CardStatusEnum
	{
		[Description("Còn bán")]
		Available = 1,

		[Description("Không còn bán")]
		Discontinued = 2,

		[Description("Đã bị xoá")]
		Deleted = 3,
	}
}
