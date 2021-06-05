using MyWebServer.Server.Http;

namespace MyWebServer.Server.Routing
{
    interface IRoutingTable
    {
        void Map(string url, HttpResponse response);
    }
}
