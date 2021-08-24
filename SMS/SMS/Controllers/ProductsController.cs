namespace SMS.Controllers
{
    using System.Linq;
    using SMS.Models.Products;
    using SMS.Services.Validation;
    using SMS.Services.Products;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class ProductsController : Controller
    {
        private readonly IValidator validator;
        private readonly IProductsService productsService;

        public ProductsController(IValidator validator, IProductsService productsService)
        {
            this.validator = validator;
            this.productsService = productsService;
        }

        [Authorize]
        public HttpResponse Create()
            => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Create(ProductCreateFormModel model)
        {
            var modelErrors = this.validator.ValidateProduct(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            this.productsService.Create(
                model.Name,
                model.Price);

            return Redirect("/Home/IndexLoggedIn");
        }
    }
}
