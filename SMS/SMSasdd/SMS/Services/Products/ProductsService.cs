namespace SMS.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using SMS.Data;
    using SMS.Data.Models;
    using SMS.Models.Products;

    public class ProductsService : IProductsService
    {
        private readonly SMSDbContext data;

        public ProductsService(SMSDbContext data)
            => this.data = data;

        public IEnumerable<ProductListingViewModel> All()
            => this.data
                .Products
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price.ToString("F2")
                })
                .ToList();

        public void Create(string name, decimal price)
        {
            this.data
                .Products
                .Add(new Product
                {
                    Name = name,
                    Price = price
                });

            this.data.SaveChanges();
        }
    }
}
