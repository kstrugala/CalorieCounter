namespace CalorieCounter.Infrastructure.Services
{
    public class ErrorCodes
    {
        public static string InvalidId => "invalid_id";
        public static string InvalidCredentials => "invalid_credentials";
        public static string EmailInUse => "email_in_use";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidFirstName => "invalid_firstname";
        public static string InvalidLastName => "invalid_lastname";
        public static string InvalidUsername => "invalid_username";
        public static string InvalidPassword => "invalid_password";
        public static string InvalidQuantity => "invalid_quantity";
        public static string UserNotFound => "user_not_found";
    }
}