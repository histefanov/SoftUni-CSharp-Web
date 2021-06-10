using MyWebServer.Server;
using MyWebServer.Server.Http;
using MyWebServer.Server.Routing;

namespace MyWebServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index() => Text("Hello from 61 blok");
    }
}
