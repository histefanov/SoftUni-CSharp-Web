namespace Git.Services.Repositories
{
    using System.Collections.Generic;
    using Git.Models.Repositories;

    public interface IRepositoryService
    {
        IEnumerable<RepositoryListingViewModel> All(string userId);

        string Create(string userId, string name, string repositoryType);

        RepositoryCommitViewModel GetRepositoryForCommit(string id);

        bool IsPublic(string id);
    }
}
