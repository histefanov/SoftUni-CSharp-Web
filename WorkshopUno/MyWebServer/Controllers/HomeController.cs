using MyWebServer.Server;
using MyWebServer.Server.Http;
using MyWebServer.Server.Routing;
using System;

namespace MyWebServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index() => Text("Hello from 61 blok");

        public HttpResponse ToYoutube() => Redirect("https://youtube.com");

        public HttpResponse StaticFiles() => View();

        public HttpResponse Error() => throw new InvalidOperationException("Invalid action!");
    }
}
