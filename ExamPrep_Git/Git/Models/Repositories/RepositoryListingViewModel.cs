﻿namespace Git.Models.Repositories
{
    using System;

    public class RepositoryListingViewModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Owner { get; init; }

        public string CreatedOn { get; init; }

        public int CommitsCount { get; init; }
    }
}
