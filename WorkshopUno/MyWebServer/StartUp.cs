namespace MyWebServer
{
    using System.Threading.Tasks;
    using MyWebServer.Server;

    public class StartUp
    {
        public static async Task Main()
        {
            var server = new MyHttpServer("127.0.0.1", 8888);

            await server.Start();
        }
    }
}
