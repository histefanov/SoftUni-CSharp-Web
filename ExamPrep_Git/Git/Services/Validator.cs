namespace Git.Services
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Git.Models.Repositories;
    using Git.Models.Users;
    using Git.Models.Commits;

    using static Data.DataConstants.User;
    using static Data.DataConstants.Repository;
    using static Data.DataConstants.Commit;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UsernameMinLength || model.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username must be between {UsernameMinLength} and {UsernameMaxLength} characters long.");
            }

            if (!Regex.IsMatch(
                model.Email, 
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                errors.Add($"{model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > PasswordMaxLength)
            {
                errors.Add($"Password must be between {PasswordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (model.ConfirmPassword != model.Password)
            {
                errors.Add($"Password and password confirmation do not match.");
            }

            return errors;
        }

        public ICollection<string> ValidateRepositoryCreating(CreateRepositoryFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < NameMinLength || model.Name.Length > NameMaxLength)
            {
                errors.Add($"Repository name must be between {NameMinLength} and {NameMaxLength} character long.");
            }

            if (model.RepositoryType != PublicRepository && model.RepositoryType != PrivateRepository)
            {
                errors.Add($"Repository type may only be public or private.");
            }

            return errors;
        }

        public bool IsCommitDescriptionValid(string description)
            => description.Length >= DescriptionMinLength;
    }
}
