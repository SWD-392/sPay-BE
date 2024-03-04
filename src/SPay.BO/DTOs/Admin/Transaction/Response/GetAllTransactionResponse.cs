using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Transaction.Response
{
    public class TransactionResponse
    {
        public string TransactionId { get; set;}
        public string Description { get; set;}
        public string Amount { get; set;}
        public string FromUser { get; set;}
        public string ToStore { get; set;}
        public string StoreOwner { get; set;}
        public string CardName {  get; set;}
        public DateTime InsDate {  get; set;}
        public string Status { get; set;}
    }
}
