using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> Query { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var firstLine = lines.First().Split();

            var method = ParseHttpMethod(firstLine[0]);

            var (path, query) = ParseUrl(firstLine[1]);           

            var headerCollection = ParseHttpHeaderCollection(lines.Skip(1));

            var bodyLines = lines.Skip(headerCollection.Count + 2).ToArray();

            var body = string.Join(Environment.NewLine, bodyLines);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headerCollection,
                Body = body
            };
        }

        private static HttpMethod ParseHttpMethod(string method)
        {
            switch (method.ToUpper())
            {
                case "GET": return HttpMethod.Get;
                case "POST": return HttpMethod.Post;
                case "PUT": return HttpMethod.Put;
                case "DELETE": return HttpMethod.Delete;
                default: throw new InvalidOperationException($"Method {method} is not supported on this server");
            }
        }

        private static HttpHeaderCollection ParseHttpHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new HttpHeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (!String.IsNullOrWhiteSpace(headerLine))
                {
                    var headerTokens = headerLine.Split(':');

                    var headerName = headerTokens[0];
                    var headerValue = headerTokens[1].Trim();

                    headerCollection.Add(headerName, headerValue);
                }
            }

            return headerCollection;
        }

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?');

            var path = urlParts[0];

            var query = urlParts.Length > 1
                ? ParseQuery(urlParts[1])
                : new Dictionary<string, string>();  

            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string qString)
        {
            var query = new Dictionary<string, string>();

            foreach (var item in qString.Split('&'))
            {
                var kvp = item.Split(new[] { '=' }, 2);

                if (kvp.Length == 2)
                {
                    query.Add(kvp[0], kvp[1]);
                }
            }

            return query;
        }
    }
}
