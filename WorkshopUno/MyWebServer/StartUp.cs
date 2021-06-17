namespace MyWebServer
{
    using System.Threading.Tasks;
    using MyWebServer.Controllers;
    using MyWebServer.Server;
    using MyWebServer.Server.Routing;

    public class StartUp
    {
        public static async Task Main()
            => await new MyHttpServer(routes => routes
                .MapGet<HumansController>("/shinobis", c => c.Shinobis())
                .MapGet<AnimalsController>("/guinea-pigs", c => c.GuineaPigs())
                .MapGet<AnimalsController>("/harambe", c => c.Harambe())
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/youtube", c => c.ToYoutube())
                .MapGet<HomeController>("/error", c => c.Error())
                .MapGet<AnimalsController>("/dogs", c => c.Dogs())
                .MapGet<AnimalsController>("/cats", c => c.Cats())
                .MapGet<CatsController>("/cats/create", c => c.Create())
                .MapPost<CatsController>("/cats/save", c => c.Save())
                .MapGet<AccountController>("/cookies", c => c.ActionWithCookies())
                .MapGet<AccountController>("/sessions", c => c.ActionWithSession()))
            .Start();
    }
}
