namespace Git.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Common;
    using static DataConstants.User;

    public class User
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        public IEnumerable<Repository> Repositories { get; init; } 
            = new HashSet<Repository>();

        public IEnumerable<Commit> Commits { get; init; } 
            = new HashSet<Commit>();
    }
}
