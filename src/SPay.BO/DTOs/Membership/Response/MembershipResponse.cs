using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Membership.Response
{
	public class MembershipResponse
	{
		public int No {  get; set; } = 0;
		public string MembershipKey { get; set; } = null!;
		public string UserKey { get; set; } = null!;
		public string CardName { get; set; } = null!;
		public string CardTypeName { get; set; } = null!;
		public string CardDescription { get; set; } = null!;
		public string StoreCateName { get; set; } = null!;
		public decimal? UsaebleAmount { get; set; }
		public decimal? Balance { get; set; }
		public bool WithdrawAllowed { get; set; }
		public DateTime? ExpiredDate { get; set; }
		public bool IsDefaultMembership { get; set; }
	}
}
