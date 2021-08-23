namespace CarShop.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class HomeController : Controller
    {
        [HttpGet]
        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Cars/All");
            }

            return this.View();
        }
    }
}
