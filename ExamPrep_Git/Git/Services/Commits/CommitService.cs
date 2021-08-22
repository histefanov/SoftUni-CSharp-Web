namespace Git.Services.Commits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Git.Data;
    using Git.Data.Models;
    using Git.Models.Commits;

    public class CommitService : ICommitService
    {
        private readonly GitDbContext data;

        public CommitService(GitDbContext data) 
            => this.data = data;

        public bool Create(string userId, string repositoryId, string description)
        {
            var repository = this.data.Repositories.Find(repositoryId);

            if (repository == null || (!repository.IsPublic && repository.OwnerId != userId))
            {
                return false;
            }

            this.data.Commits.Add(new Commit
            {
                CreatorId = userId,
                RepositoryId = repositoryId,
                Description = description,
                CreatedOn = DateTime.UtcNow
            });

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<CommitListingViewModel> All(string userId)
            => this.data
                .Commits
                .Where(c => c.CreatorId == userId)
                .Select(c => new CommitListingViewModel
                {
                    Id = c.Id,
                    RepositoryName = c.Repository.Name,
                    Description = c.Description,
                    CreatedOn = c.CreatedOn.ToLocalTime().ToString("F")
                })
                .ToList();

        public bool Delete(string userId, string id)
        {
            var commit = this.data
                .Commits
                .FirstOrDefault(c => c.Id == id && c.CreatorId == userId);

            if (commit == null)
            {
                return false;
            }

            this.data
                .Commits
                .Remove(commit);

            this.data.SaveChanges();

            return true;
        }
    }
}
