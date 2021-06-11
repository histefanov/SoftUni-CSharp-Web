using MyWebServer.Server;
using MyWebServer.Server.Http;
using MyWebServer.Server.Routing;

namespace MyWebServer.Controllers
{
    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Cats() => Html($"<h1>Hello from the cats!</h1>");

        public HttpResponse Dogs() => View("Animals/Dogs");

        public HttpResponse GuineaPigs() => View("GuineaPigs");

        public HttpResponse Harambe() => View();
    }
}
