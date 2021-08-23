namespace CarShop.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using CarShop.Models.Cars;
    using CarShop.Models.Users;

    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(UserRegistrationFormModel model)
        {
            var modelErrors = new List<string>();

            if (model.Username.Length < 4 || model.Username.Length > 20)
            {
                modelErrors.Add($"Username must be between {UsernameMinLength} and {UsernameMaxLength} characters.");
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                modelErrors.Add("The email field is required.");
            }
            else if (!Regex.IsMatch(model.Email, EmailRegex))
            {
                modelErrors.Add($"{model.Email} is not a valid email address.");
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > PasswordMaxLength)
            {
                modelErrors.Add($"Password must be between {PasswordMinLength} and {PasswordMaxLength} characters.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                modelErrors.Add("Your password and confirmation password do not match");
            }

            if (model.UserType != ClientUserType && model.UserType != MechanicUserType)
            {
                modelErrors.Add($"{model.UserType} is not a valid user type.");
            }

            return modelErrors;
        }

        public ICollection<string> ValidateCar(CarAddFormModel model)
        {
            var modelErrors = new List<string>();

            if (model.Model.Length < CarModelMinLength || model.Model.Length > CarModelMaxLength)
            {
                modelErrors.Add($"Car model must be between {CarModelMinLength} and {CarModelMaxLength} characters.");
            }

            if (model.Year == 0)
            {
                modelErrors.Add($"The year field is required.");
            }

            if (!Regex.IsMatch(model.PlateNumber, CarPlateNumberRegex))
            {
                modelErrors.Add($"Plate number must be in the format 'AA0000AA'");
            }

            return modelErrors;
        }

        public bool IsIssueDescriptionValid(string description)
            => description.Length > IssueDescriptionMinLength;
    }
}
