namespace SMS.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Common;

    public class User
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Username { get; init; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(IdMaxLength)]
        public string CartId { get; init; }

        public Cart Cart { get; init; }
    }
}
