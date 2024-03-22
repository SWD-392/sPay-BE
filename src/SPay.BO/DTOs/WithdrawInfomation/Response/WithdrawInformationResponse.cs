using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.WithdrawInfomation.Response
{
	public class WithdrawInformationResponse
	{
		public int No { get; set; } = 0;
		public string WithdrawKey { get; set; } = null!;
		public string Type { get; set; } = null!;
		public string UserKey { get; set; } = null!;
		public string UserName { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;	
		public string Description { get; set; } = null!;
		public decimal? TotalAmount { get; set; }
		public string Status { get; set; } = null!;
		public DateTime InsDate { get; set; }
	}
}
