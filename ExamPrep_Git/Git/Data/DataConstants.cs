namespace Git.Data
{
    public class DataConstants
    {
        public class Common
        {
            public const int IdMaxLength = 40;
        }

        public class User
        {
            public const int UsernameMinLength = 5;
            public const int UsernameMaxLength = 20;

            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 20;
        }

        public class Repository
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 10;

            public const string PublicRepository = "Public";
            public const string PrivateRepository = "Private";
        }

        public class Commit
        {
            public const int DescriptionMinLength = 5;
        }
    }
}
