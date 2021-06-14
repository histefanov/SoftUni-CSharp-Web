using MyWebServer.Models.Animals;
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

        public HttpResponse Cats()
        {
            const string NameKey = "Name";
            const string AgeKey = "Age";
            const string BreedKey = "Breed";   

            var query = this.Request.Query;

            var catName = query.ContainsKey(NameKey)
                ? query[NameKey]
                : "the cats";

            var catAge = query.ContainsKey(AgeKey)
                ? int.Parse(query[AgeKey])
                : 0;

            var catBreed = query.ContainsKey(BreedKey)
                ? query[BreedKey]
                : "";

            var viewModel = new CatViewModel
            {
                Name = catName,
                Age = catAge,
                Breed = catBreed
            };

            return View(viewModel);
        }

        public HttpResponse Dogs() => View();

        public HttpResponse GuineaPigs() => View("GuineaPigs");

        public HttpResponse Harambe() => View("Animals/Rip/Harambe");
    }
}
