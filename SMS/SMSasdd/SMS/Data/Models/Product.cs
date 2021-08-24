namespace SMS.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Common;
    using static DataConstants.Product;

    public class Product
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Name { get; set; }

        [Range((double)MinPrice, (double)MaxPrice)]
        public decimal Price { get; set; }

        [MaxLength(IdMaxLength)]
        public string CartId { get; set; }

        public Cart Cart { get; init; }
    }
}
