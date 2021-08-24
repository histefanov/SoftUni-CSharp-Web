namespace SMS.Services.Carts
{
    using System.Linq;
    using SMS.Data;
    using SMS.Data.Models;
    using SMS.Models.Carts;
    using SMS.Models.Products;

    public class CartsService : ICartsService
    {
        private readonly SMSDbContext data;

        public CartsService(SMSDbContext data)
            => this.data = data;

        public string Create()
        {
            var cart = new Cart();

            this.data.Add(cart);
            this.data.SaveChanges();

            return cart.Id;
        }

        public bool AddProduct(string productId, string cartId)
        {
            var product = this.data
                .Products
                .FirstOrDefault(p => p.Id == productId);

            if (product == null || product.CartId != null)
            {
                return false;
            }

            product.CartId = cartId;

            this.data.SaveChanges();

            return true;
        }

        public CartViewModel Details(string userId)
        {
            var cartId = this.GetCartId(userId);

            if (cartId == null)
            {
                return null;
            }

            var cart = this.data
                .Carts
                .Where(c => c.Id == cartId)
                .Select(c => new CartViewModel
                {
                    Id = cartId
                })
                .FirstOrDefault();

            cart.Products = this.data
                .Products
                .Where(p => p.CartId == cartId)
                .Select(p => new ProductListingViewModel
                {
                    Name = p.Name,
                    Price = p.Price.ToString("F2")
                });

            return cart;
        }

        public string GetCartId(string userId)
            => this.data
                .Carts
                .Where(c => c.User.Id == userId)
                .Select(c => c.Id)
                .FirstOrDefault();

        public void Buy(string cartId)
        {
            var products = this.data
                .Products
                .Where(p => p.CartId == cartId)
                .ToList();

            this.data.Products.RemoveRange(products);
            this.data.SaveChanges();
        }
    }
}
