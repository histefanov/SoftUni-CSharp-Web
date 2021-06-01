using System.Threading.Tasks;
using MyWebServer.Server;

namespace MyWebServer
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        {
            var server = new MyHttpServer("127.0.0.1", 8888);

            await server.Start();
        }
    }
}
