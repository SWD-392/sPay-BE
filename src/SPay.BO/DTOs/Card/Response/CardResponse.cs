using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Card.Response
{
    public class CardResponse
    {
        public int No { get; set; }
        public string CardKey { get; set; } = null!;
        public string CardTypeKey { get; set; } = null!;
        public string CardTypeName { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal? MoneyValue { get; set; }
        public byte DiscountPercentage { get; set; }
        public decimal? Price { get; set; }
        public DateTime InsDate { get; set; }
        public int DateNumber { get; set; }
        public byte Status { get; set; }
    }
}
