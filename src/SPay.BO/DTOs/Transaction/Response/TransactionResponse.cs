using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Transaction.Response
{
	public class TransactionResponse
	{
		public int No {  get; set; } = 0;
		public string Type { get; set; } = null!;
		public decimal Amount { get; set; }
		public string Sender { get; set; } = null!;
		public string Receiver { get; set; } = null!;
		public string DescriptionTrans { get; set; } = null!;
		public string DescriptionDetails { get; set; } = null!;
		public string Status { get; set; } = null!;
		public DateTime TransactionDate { get; set; }
	}
}
