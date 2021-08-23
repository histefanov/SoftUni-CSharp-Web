namespace CarShop.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;

        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 20;
        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 20;
        public const string EmailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const string ClientUserType = "Client";
        public const string MechanicUserType = "Mechanic";

        public const int CarModelMinLength = 5;
        public const int CarModelMaxLength = 20;
        public const int CarPlateNumberMaxLength = 8;
        public const string CarPlateNumberRegex = @"[A-Z]{1,2}\d{4}[A-Z]{2}";

        public const int IssueDescriptionMinLength = 5;
    }
}
