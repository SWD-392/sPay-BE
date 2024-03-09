using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Card.Response
{
    public class CardResponse
    {
        public int No { get; set; }
        public string CardKey { get; set; }
        public string CardTypeKey { get; set; }
        public string CardTypeName { get; set; }
        public string CardName { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
		public decimal? MoneyValue { get; set; }
        public byte DiscountPercentage { get; set; }
		public decimal? Price { get; set; }
        public DateTime InsDate { get; set; }
        public int DateNumber { get; set; }
        public byte Status { get; set; }
    }
}
