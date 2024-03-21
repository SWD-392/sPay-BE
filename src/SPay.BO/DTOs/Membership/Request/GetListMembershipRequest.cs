using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Membership.Request
{
	public class GetListMembershipRequest : PagingRequest
	{
		public string? UserKey { get; set; } = null;
		public string? StoreCateKey { get; set; } = null;
	}
}
