using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;
using System.Collections.Generic;

namespace MyWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable()
        {
            this.routes = new Dictionary<HttpMethod, Dictionary<string, HttpResponse>>
            {
                [HttpMethod.Get] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.Post] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.Put] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.Delete] = new Dictionary<string, HttpResponse>()
            };
        }

        public IRoutingTable Map(
            string url, 
            HttpMethod method, 
            HttpResponse response)
        {
            Guard.AgainstNull(url);
            Guard.AgainstNull(method);
            Guard.AgainstNull(response);

            this.routes[method][url] = response;

            return this;
        }

        public IRoutingTable MapGet(
            string url, 
            HttpResponse response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this.routes[HttpMethod.Get][url] = response;

            return this;
        }

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if (!this.routes.ContainsKey(requestMethod)
                || !this.routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }

            return this.routes[requestMethod][requestUrl];
        }
    }
}
