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

        public HttpResponse LogIn()
        {
            //var user = this.db.Users.Find(username, password);

            //if (user != null)
            //{
            //    this.SignIn(user.Id);
            //    return Text("User is logged in!");
            //}
            //
            //return Text("Invalid credentials!");

            var someUserId = "MyUserId"; // should come from the database

            this.SignIn(someUserId);

            return Text("User authenticated!");
        }

        public HttpResponse LogOut()
        {
            this.SignOut();

            return Text("User signed out!");
        }

        public HttpResponse AuthenticationCheck()
        {
            if (this.User.IsAuthenticated)
            {
                return Text($"Authenticated user: {this.User.Id}");
            }

            return Text($"User is not authenticated.");
        }

        public ActionResult CookiesCheck()
        {
            const string cookieName = "My-Cookie";

            if (this.Request.Cookies.ContainsKey(cookieName))
            {
                var cookie = this.Request.Cookies[cookieName];

                return Text($"Cookies already exist - {cookie}");
            }

            this.Response.AddCookie("My-Cookie", "My-Value");
            this.Response.AddCookie("My-Second-Cookie", "My-Value");

            return Text("Cookies set! Hello from the cookie monster.");
        }

        public ActionResult SessionCheck()
        {
            const string currentDateKey = "CurrentDate";

            if (this.Request.Session.ContainsKey(currentDateKey))
            {
                var currentDate = this.Request.Session[currentDateKey];

                return Text($"Stored date: {currentDate}");
            }

            this.Request.Session[currentDateKey] = DateTime.UtcNow.ToString();

            return Text("Current date stored!");
        }
    }
}
