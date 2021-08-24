namespace SMS.Controllers
{
    using SMS.Services.Carts;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Services.Validation.ErrorMessages.Products;

    public class CartsController : Controller
    {
        private readonly ICartsService cartsService;

        public CartsController(ICartsService cartsService) 
            => this.cartsService = cartsService;

        [Authorize]
        public HttpResponse AddProduct(string productId)
        {
            var cartId = this.cartsService.GetCartId(this.User.Id);

            var isSuccess = this.cartsService.AddProduct(productId, cartId);

            if (!isSuccess)
            {
                return Error(ItemAddFailedMessage);
            }

            return Redirect("/Carts/Details");
        }

        [Authorize]
        public HttpResponse Details()
        {
            var cart = this.cartsService.Details(this.User.Id);

            return View(cart);
        }

        [Authorize]
        public HttpResponse Buy()
        {
            var cartId = this.cartsService.GetCartId(this.User.Id);

            this.cartsService.Buy(cartId);

            return Redirect("/Home/IndexLoggedIn");
        }
    }
}
