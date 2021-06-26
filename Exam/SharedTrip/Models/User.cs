namespace SharedTrip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class User
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [MaxLength(DefaultMaxLength)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        //[MaxLength(DefaultMaxLength)]
        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserTrip> UserTrips { get; init; } = new HashSet<UserTrip>();
    }
}
