using MyWebServer.Server.Http;

namespace MyWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method, string url, HttpResponse response);

        IRoutingTable MapGet(string url, HttpResponse response);
    }
}
