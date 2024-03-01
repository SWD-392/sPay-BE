using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.RerferenceSRC.DTOs.Response.Category
{
    public class GetCategoryResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public GetCategoryResponse()
        {

        }

        public GetCategoryResponse(int CategoryId, string CategoryName, string Description)
        {
            this.CategoryId = CategoryId;
            this.CategoryName = CategoryName;
            this.Description = Description;
        }
    }
}
