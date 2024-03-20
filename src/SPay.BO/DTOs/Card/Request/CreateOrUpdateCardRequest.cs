﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Card.Request
{
    public class CreateOrUpdateCardRequest
    {
        public string CardTypeKey { get; set; } = null!;
        public string? CardNumber { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
		public string PromotionPackageKey { get; set; } = null!;
	}
}
