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
		public string? CardKey { get; set; }
		public bool IsDefaultMembership { get; set; }
	}
}
