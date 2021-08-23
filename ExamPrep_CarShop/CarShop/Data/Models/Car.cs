namespace CarShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Car
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        [MaxLength(CarPlateNumberMaxLength)]
        public string PlateNumber { get; set; }

        [Required]
        [MaxLength(IdMaxLength)]
        public string OwnerId { get; set; }

        public User Owner { get; init; }

        public IEnumerable<Issue> Issues { get; init; } = new HashSet<Issue>();
    }
}
