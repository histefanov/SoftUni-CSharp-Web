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
            const string CookieName = "My-Cookie";

            if (this.Request.Cookies.ContainsKey(CookieName))
            {
                var cookie = this.Request.Cookies[CookieName];

                return Text($"Cookies already exist - {cookie}");
            }

            this.Response.AddCookie("My-Cookie", "My-Value");
            this.Response.AddCookie("My-Second-Cookie", "My-Value");

            return Text("Cookies set! Hello from the cookie monster.");
        }
    }
}
