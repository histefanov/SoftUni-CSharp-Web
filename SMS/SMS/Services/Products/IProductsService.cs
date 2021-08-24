namespace SMS.Services.Products
{
    using System.Collections.Generic;
    using SMS.Models.Products;

    public interface IProductsService
    {
        IEnumerable<ProductListingViewModel> All();

        void Create(string name, decimal price);
    }
}
