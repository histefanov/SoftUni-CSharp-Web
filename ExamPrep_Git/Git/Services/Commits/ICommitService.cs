namespace Git.Services.Commits
{
    using Git.Models.Commits;
    using System.Collections.Generic;

    public interface ICommitService
    {
        bool Create(string userId, string repositoryId, string description);

        IEnumerable<CommitListingViewModel> All(string userId);

        bool Delete(string userId, string id);
    }
}
