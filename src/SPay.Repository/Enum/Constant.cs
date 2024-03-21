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
			public const string DES_FOR_DEFAULT_MEMBERSHIP = "Ví sử dụng cho thẻ {0}";
			public const string DES_FOR_OTHER_MEMBERSHIP = "The email has already used";
			public const decimal DEFAULT_BALANCE = 0;
		}

		public static class Role
		{
			public const string CUSTOMER_ROLE = "Customer";
			public const string STORE_ROLE = "Store";
		}
	}
}
