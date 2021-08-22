namespace Git.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Common;
    using static DataConstants.Repository;

    public class Repository
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Required]
        public DateTime CreatedOn { get; init; }

        public bool IsPublic { get; init; }

        public string OwnerId { get; init; }

        public User Owner { get; init; }

        public IEnumerable<Commit> Commits { get; init; } =
            new HashSet<Commit>();
    }
}