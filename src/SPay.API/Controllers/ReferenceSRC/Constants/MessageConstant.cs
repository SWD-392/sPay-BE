namespace SPay.API.Controllers.ReferenceSRC.Constants
{
    public class MessageConstant
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
    }
}
