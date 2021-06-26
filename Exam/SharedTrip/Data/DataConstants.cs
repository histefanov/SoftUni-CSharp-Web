namespace SharedTrip.Data
{
    public class DataConstants
    {
        public const int DefaultMaxLength = 20;
        public const int DescriptionMaxLength = 80;
        public const int SeatsMinValue = 2;
        public const int SeatsMaxValue = 6;
        public const int UsernameMinLength = 5;
        public const int PasswordMinLength = 6;

        public const string EmailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        public const string DateTimeRegex = @"\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}";
    }
}
