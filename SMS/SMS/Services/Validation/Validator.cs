namespace SMS.Services.Validation
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using SMS.Models.Products;
    using SMS.Models.Users;

    using static Data.DataConstants.Common;
    using static Data.DataConstants.User;
    using static Data.DataConstants.Product;
    using static ErrorMessages.Users;
    using static ErrorMessages.Products;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(UserRegistrationFormModel model)
        {
            var modelErrors = new List<string>();

            if (model.Username.Length < UsernameMinLength || model.Username.Length > DefaultMaxLength)
            {
                modelErrors.Add(
                    string.Format(UsernameLengthMessage, UsernameMinLength, DefaultMaxLength));
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                modelErrors.Add(EmailRequiredMessage);
            }
            else if (!Regex.IsMatch(model.Email, EmailRegex))
            {
                modelErrors.Add(
                    string.Format(InvalidEmailMessage, model.Email));
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > DefaultMaxLength)
            {
                modelErrors.Add(
                    string.Format(PasswordLengthMessage, PasswordMinLength, DefaultMaxLength));
            }

            if (model.Password != model.ConfirmPassword)
            {
                modelErrors.Add(PasswordsDontMatchMessage);
            }

            return modelErrors;
        }

        public ICollection<string> ValidateProduct(ProductCreateFormModel model)
        {
            var modelErrors = new List<string>();

            if (model.Name.Length < ProductNameMinLength || model.Name.Length > DefaultMaxLength)
            {
                modelErrors.Add(
                    string.Format(NameLengthMessage, ProductNameMinLength, DefaultMaxLength));
            }

            if (model.Price < MinPrice || model.Price > MaxPrice)
            {
                modelErrors.Add(
                    string.Format(PriceRangeMessage, MinPrice, MaxPrice));
            }

            return modelErrors;
        }
    }
}
