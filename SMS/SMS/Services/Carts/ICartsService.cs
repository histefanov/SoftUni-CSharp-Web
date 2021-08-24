namespace SMS.Services.Carts
{
    using SMS.Models.Carts;

    public interface ICartsService
    {        
        string Create();

        bool AddProduct(string productId, string cartId);

        CartViewModel Details(string userId);

        string GetCartId(string userId);

        void Buy(string cartId);
    }
}
