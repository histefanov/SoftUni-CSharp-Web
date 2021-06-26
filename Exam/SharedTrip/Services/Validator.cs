namespace SharedTrip.Services
{
    using SharedTrip.Models.Trips;
    using SharedTrip.Models.Users;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateTripCreation(AddTripFormModel model)
        {
            var errors = new List<string>();

            if (model.Seats < SeatsMinValue || model.Seats > SeatsMaxValue)
            {
                errors.Add($"Number of seats must be between {SeatsMinValue} and {SeatsMaxValue}.");
            }

            if (!Regex.IsMatch(model.Date, DateTimeRegex))
            {
                errors.Add($"Departure time should be in format 'DD.MM.YYYY HH:mm'.");
            }

            if (model.Description.Length > 80)
            {
                errors.Add("Description can be up to 80 characters long.");
            }

            if (Uri.IsWellFormedUriString(model.CarImage, UriKind.Absolute))
            {
                errors.Add($"{model.CarImage} is not a valid URL.");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(UserRegistrationFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UsernameMinLength || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username must be between {UsernameMinLength} and {DefaultMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, EmailRegex))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"Password is not valid. It must be between {PasswordMinLength} and {DefaultMaxLength} characters.");
            }

            if (model.ConfirmPassword != model.Password)
            {
                errors.Add("Password and password confirmation do not match.");
            }

            return errors;
        }
    }
}
