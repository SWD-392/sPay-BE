﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.WithdrawInfomation.Request
{
	public class GetListWithdrawInfomationRequest : PagingRequest
	{
		public string? PhoneNumber { get; set; } = null;
		public string? UserName { get; set; } = null;


	}
}
