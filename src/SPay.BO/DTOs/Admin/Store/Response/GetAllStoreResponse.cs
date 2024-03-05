using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Store.Response
{
    public class StoreResponse
    {
        public int No { get; set; }
        public string StoreKey { get; set; }
        public string StoreName { get; set; }
        public string StoreCategory { get; set; }
        public string StoreCategoryKey { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Balance { get; set; }
        public string InsDate { get; set; }
        public bool? Status { get; set; }
    }
}
