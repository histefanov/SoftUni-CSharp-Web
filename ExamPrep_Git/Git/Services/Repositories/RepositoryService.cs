namespace Git.Services.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Git.Data.Models;
    using Git.Data;

    using static Data.DataConstants.Repository;
    using Git.Models.Repositories;

    public class RepositoryService : IRepositoryService
    {
        private readonly GitDbContext data;

        public RepositoryService(GitDbContext data)
            => this.data = data;

        public IEnumerable<RepositoryListingViewModel> All(string userId)
        {
            var repositoriesQuery = this.data
                .Repositories
                .AsQueryable();

            if (userId != null)
            {
                repositoriesQuery = repositoriesQuery
                    .Where(r => r.IsPublic || r.OwnerId == userId);
            }
            else
            {
                repositoriesQuery = repositoriesQuery
                    .Where(r => r.IsPublic);
            }

            var repositories = repositoriesQuery
            .OrderByDescending(r => r.CreatedOn)
            .Select(r => new RepositoryListingViewModel
            {
                Id = r.Id,
                Name = r.Name,
                CreatedOn = r.CreatedOn.ToLocalTime().ToString("F"),
                Owner = r.Owner.Username,
                CommitsCount = r.Commits.Count()
            })
            .ToList();

            return repositories;
        }

        public string Create(string userId, string name, string repositoryType)
        {
            var repository = new Repository
            {
                Name = name,
                IsPublic = repositoryType == PublicRepository,
                CreatedOn = DateTime.UtcNow,
                OwnerId = userId
            };

            this.data.Repositories.Add(repository);
            this.data.SaveChanges();

            return repository.Id;
        }

        public RepositoryCommitViewModel GetRepositoryForCommit(string id)
            => this.data
                .Repositories
                .Where(r => r.Id == id)
                .Select(r => new RepositoryCommitViewModel
                {
                    RepositoryId = r.Id,
                    Name = r.Name
                })
                .FirstOrDefault();

        public bool IsPublic(string id)
            => this.data
                .Repositories
                .Any(r => r.Id == id && r.IsPublic);
    }
}
