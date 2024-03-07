using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Service.Utils
{
	public static class DateTimeHelper
	{
		private static readonly string UTC_PLUS7_IN_VIETNAM = "SE Asia Standard Time";
		private static readonly TimeZoneInfo VietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById(UTC_PLUS7_IN_VIETNAM);

		public static DateTime GetDateTimeNow()
		{
			return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, VietnamTimeZone);
		}
	}
}
