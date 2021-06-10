namespace MyWebServer
{
    using System.Threading.Tasks;
    using MyWebServer.Controllers;
    using MyWebServer.Server;
    using MyWebServer.Server.Responses;
    using MyWebServer.Server.Routing;

    public class StartUp
    {
        public static async Task Main()
            => await new MyHttpServer(routes => routes
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<AnimalsController>("/dogs", c => c.Dogs())
                .MapGet<AnimalsController>("/cats", c => c.Cats()))
            .Start();
    }
}
