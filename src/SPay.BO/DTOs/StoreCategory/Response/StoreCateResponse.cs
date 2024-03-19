using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.StoreCategory.Response
{
	public class StoreCateResponse
	{
		public int No {  get; set; } = 0;
		public string StoreCategoryKey { get; set; } = null!;
		public string CategoryName { get; set; } = null!;
		public string? Description { get; set; }
	}
}
