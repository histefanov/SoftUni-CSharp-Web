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
            HttpMethod method,
            string path,
            HttpResponse response)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(response, nameof(response));

            this.routes[method][path] = response;

            return this;
        }

        public IRoutingTable MapGet(
            string path,
            HttpResponse response)
            => this.Map(HttpMethod.Get, path, response);

        public IRoutingTable MapPost(
            string path,
            HttpResponse response)
            => this.Map(HttpMethod.Post, path, response);

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            if (!this.routes.ContainsKey(requestMethod)
                || !this.routes[requestMethod].ContainsKey(requestPath))
            {
                return new NotFoundResponse();
            }

            return this.routes[requestMethod][requestPath];
        }
    }
}
