using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.Enum
{
	public class Constant
	{
		public static class LoginMessage
		{
			public const string InvalidEmailOrPassword = "Incorrect email or password!";
			public const string AccountIsUnavailable = "Your account has been disabled!";
		}

		public static class SignUpMessage
		{
			public const string EmailHasAlreadyUsed = "The email has already used";
		}

		public static class Wallet
		{
			public const string DES_FOR_DEFAULT_MEMBERSHIP = "Ví dành cho membership default";
			public const string DES_FOR_OTHER_MEMBERSHIP = "Ví sử dụng cho thẻ {0}";
			public const string DES_FOR_STORE = "Ví sử dụng cho cửa hàng {0}";
			public const decimal DEFAULT_BALANCE = 0;
		}

		public static class Role
		{
			public const string CUSTOMER_ROLE = "Customer";
			public const string STORE_ROLE = "Store";
		}

		public static class Transaction
		{
			public const string DES_FOR_WITHDRAWL = "{0}: {1} đã rút tiền. Số tiền {2}";
			public const string DES_FOR_PURCHASE = "{0}: {1} đã thanh toáncho cửa hàng {2}. Số tiền: {3}";
			public const string TYPE_PURCHASE = "Chuyển tiền";
			public const string TYPE_WITHDRAWL = "Rút tiền";
			public const string UNDEFINE_STR = "Không xác định";
			public const string WITHDRAW_SENDER = "ADMIN";
			public const string WITHDRAW_DETAILS_DES = "Giao dịch rút tiền";
			public const decimal UNDEFINE_AMOUNT = -1;
		}
	}
}
