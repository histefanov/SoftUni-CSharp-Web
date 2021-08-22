namespace Git.Services
{
    using System.Collections.Generic;
    using Git.Models.Commits;
    using Git.Models.Repositories;
    using Git.Models.Users;

    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);

        ICollection<string> ValidateRepositoryCreating(CreateRepositoryFormModel model);

        bool IsCommitDescriptionValid(string description);
    }
}
