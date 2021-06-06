using MyWebServer.Server.Http;

namespace MyWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        public IRoutingTable Map(string url, HttpMethod method, HttpResponse response)
        {
            throw new System.NotImplementedException();
        }

        public IRoutingTable MapGet(string url, HttpResponse response)
        {
            throw new System.NotImplementedException();
        }
    }
}
