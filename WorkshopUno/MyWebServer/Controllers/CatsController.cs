using MyWebServer.Server;
using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Controllers
{
    public class CatsController : Controller
    {
        public CatsController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Create() => View("Animals/Cats/Create");

        public HttpResponse Save()
        {
            var name = this.Request.Form["Name"];
            var age = this.Request.Form["Age"];
            var breed = this.Request.Form["Breed"];

            return Text($"{name} ({breed}) - {age} years old.");
        }
    }
}
