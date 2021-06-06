namespace MyWebServer
{
    using System.Threading.Tasks;
    using MyWebServer.Server;
    using MyWebServer.Server.Responses;

    public class StartUp
    {
        public static async Task Main()
            => await new MyHttpServer(routes => routes
                .MapGet("/", new TextResponse("Hello from 61 blok"))
                .MapGet("/Dogs", new TextResponse("Hello from Regon and Sara")))
            .Start();
    }
}
