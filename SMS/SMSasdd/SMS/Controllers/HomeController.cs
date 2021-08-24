namespace SMS.Controllers
{
    using SMS.Services.Products;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService) 
            => this.productsService = productsService;

        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Home/IndexLoggedIn");
            }

            return View();
        }
        
        [Authorize]
        public HttpResponse IndexLoggedIn()
        {
            var products = this.productsService.All();

            return View(products);
        }
    }
}