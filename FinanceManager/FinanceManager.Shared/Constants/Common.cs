namespace FinanceManager.Shared.Constants
{
    public static class Common
    {
        public const string ApiEndpoint = "https://localhost:7258";
        public const string LoginEndpoint = "/api/auth/login";
        public const string RegistrationEndpoint = "/api/users/register";
        public const string CreationUsername = "user.name@mail.com";
        public const string ModificationUsername = "user.name@mail.com";
        public const string GenericErrorMessage = "An unexpected error occurred";
        public const string GenericValidationMessage = "Validation failed";
        public const string GenericAuthorizationMessage = "You are not authorized to perform this action";
        public const int WindowClosingDelay = 2000;
    }
}