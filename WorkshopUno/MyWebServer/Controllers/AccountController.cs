using MyWebServer.Server;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) 
            : base(request)
        {
        }

        public ActionResult ActionWithCookies()
        {

        }
    }
}
