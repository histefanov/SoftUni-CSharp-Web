using MyWebServer.Server;
using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Controllers
{
    public class HumansController : Controller
    {
        public HumansController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Shinobis() => View("Humans/Shinobis");
    }
}
