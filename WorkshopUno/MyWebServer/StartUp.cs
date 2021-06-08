namespace MyWebServer
{
    using System.Threading.Tasks;
    using MyWebServer.Server;
    using MyWebServer.Server.Responses;

    public class StartUp
    {
        public static async Task Main()
            => await new MyHttpServer(routes => routes
                .MapGet("/", new HtmlResponse("Hello from 61 blok"))
                .MapGet("/dogs", new HtmlResponse("<h1>Hello from Regon and Sara</h1>"))
                .MapGet("/bro", new ContentResponse("<h2>what's up my g</h2>", "text/html; charset=UTF-8")))
            .Start();
    }
}
