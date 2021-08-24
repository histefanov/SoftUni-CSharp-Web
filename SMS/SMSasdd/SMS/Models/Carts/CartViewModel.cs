namespace SMS.Models.Carts
{
    using System.Collections.Generic;
    using SMS.Models.Products;

    public class CartViewModel
    {
        public string Id { get; init; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
