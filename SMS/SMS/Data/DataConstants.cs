namespace SMS.Data
{
    public class DataConstants
    {
        public class Common
        {
            public const int IdMaxLength = 40;
            public const int DefaultMaxLength = 20;
        }

        public class User
        {
            public const int UsernameMinLength = 5;
            public const string EmailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            public const int PasswordMinLength = 6; 
        }

        public class Product
        {
            public const int ProductNameMinLength = 4;
            public const decimal MinPrice = 0.05m;
            public const decimal MaxPrice = 1000m;
        }
    }
}
