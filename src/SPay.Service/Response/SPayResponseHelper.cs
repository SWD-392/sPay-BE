using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Service.Response
{
	public static class SPayResponseHelper
	{
		public const string NOT_FOUND = "404";

		// Helper method to set error response properties
		public static void SetErrorResponse<T>(SPayResponse<T> resp, string message, string? errorMessage = null)
		{
			if(resp.Data == null)
			{
				resp.Error = NOT_FOUND;
			}
			resp.Success = false;
			resp.Message = message;
			if (errorMessage != null)
			{
				resp.ErrorMessages = new List<string> { errorMessage };
			}
			else
			{
				resp.ErrorMessages = new List<string>();
			}
		}
	}
}
