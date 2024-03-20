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

		public static string GenerateUniqueQRCode()
		{
			// Sử dụng thời gian hiện tại để tạo ra một chuỗi ngẫu nhiên
			string timeStamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, VietnamTimeZone).ToString("yyyyMMddHHmmssfff");

			// Tạo ra một số ngẫu nhiên
			Random random = new Random();
			int randomNumber = random.Next(1000, 9999); // Số ngẫu nhiên từ 1000 đến 9999

			// Kết hợp thời gian và số ngẫu nhiên để tạo ra mã QR
			string qrCode = timeStamp + randomNumber.ToString();

			return qrCode;
		}
	}
}
