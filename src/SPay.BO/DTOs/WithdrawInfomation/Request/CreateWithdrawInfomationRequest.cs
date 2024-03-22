using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.WithdrawInfomation.Request
{
	public class CreateWithdrawInfomationRequest
	{
		public string UserKey { get; set; } = null!;
		public decimal? TotalAmount { get; set; }
	}
}
