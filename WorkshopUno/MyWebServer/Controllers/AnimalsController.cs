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

        public HttpResponse Dogs()
        {
            const string nameKey = "name";
            var query = this.Request.Query;

            var dogName = query.ContainsKey(nameKey) ? query[nameKey] : "the dogs";

            var result = $"<h1>Hello from {dogName}</h1>";

            return Html(result);
        }

        public HttpResponse Cats() => Html($"<h1>Hello from the cats!</h1>");
    }
}
