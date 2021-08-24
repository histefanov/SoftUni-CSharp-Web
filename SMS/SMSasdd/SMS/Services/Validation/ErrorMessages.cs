namespace SMS.Services.Validation
{
    public class ErrorMessages
    {
        public class Users
        {
            public const string InvalidCredentialsMessage = "Invalid username or password";
            public const string UsernameUnavailableMessage = "Username is already taken.";
            public const string UsernameLengthMessage = "Username must be between {0} and {1} characters.";
            public const string EmailRequiredMessage = "The email field is required.";
            public const string InvalidEmailMessage = "{0} is not a valid email address.";
            public const string PasswordLengthMessage = "Password must be between {0} and {1} characters.";
            public const string PasswordsDontMatchMessage = "Your password and confirmation password do not match.";
        }

        public class Products
        {
            public const string ItemAddFailedMessage = "Item does not exist or is already in another user's cart.";
            public const string NameLengthMessage = "Product name must be between {0} and {1} characters.";
            public const string PriceRangeMessage = "Product price must be between {0} and {1}";
        }
    }
}
