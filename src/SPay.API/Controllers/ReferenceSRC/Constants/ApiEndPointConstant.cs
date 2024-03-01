
namespace SPay.API.Controllers.ReferenceSRC.Constants
{
    public class ApiEndPointConstant
    {
        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndPoint = RootEndPoint + ApiVersion;

        public static class Account
        {
            public const string AccountsEndPoint = ApiEndPoint + "/accounts";
            public const string AccountEndPoint = AccountsEndPoint + "/{id}";
            public const string AccountStatusEndPoint = AccountEndPoint + "/status";
        }

        public static class Authentication
        {
            public const string AuthenticationEndpoint = ApiEndPoint + "/auth";
            public const string LoginEndPoint = AuthenticationEndpoint + "/login";
            public const string SignUpEndPoint = AuthenticationEndpoint + "/signup";
        }

        public static class Role
        {
            public const string RolesEndPoint = ApiEndPoint + "/roles";
            public const string RoleEndPoint = RolesEndPoint + "/{id}";
        }

        public static class Course
        {
            public const string CoursesEndPoint = ApiEndPoint + "/courses";
            public const string CourseEndPoint = CoursesEndPoint + "/{id}";
            public const string CourseStatusEndPoint = CourseEndPoint + "/status";
        }

        public static class Category
        {
            public const string CategoriesEndPoint = ApiEndPoint + "/category";
            public const string CategoryEndPoint = CategoriesEndPoint + "/id";
        }
    }
}
