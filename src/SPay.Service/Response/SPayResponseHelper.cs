using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Service.Response
{
	public static class SPayResponseHelper
	{
		// Helper method to set error response properties
		public static void SetErrorResponse<T>(SPayResponse<T> resp, string message, string errorMessage = null)
		{
			resp.Success = false;
			resp.Message = message;
			resp.ErrorMessages = new List<string> { errorMessage };
		}
	}
}
