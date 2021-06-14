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

        public IReadOnlyDictionary<string, string> Query { get; private set; }

        public IReadOnlyDictionary<string, HttpHeader> Headers { get; private set; }

        public IReadOnlyDictionary<string, string> Form { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var firstLine = lines.First().Split();

            var method = ParseHttpMethod(firstLine[0]);

            var (path, query) = ParseUrl(firstLine[1]);

            var headers = ParseHttpHeaderCollection(lines.Skip(1));

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(Environment.NewLine, bodyLines);

            var form = ParseForm(headers, body);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headers,
                Body = body
            };
        }

        private static Dictionary<string, string> ParseForm(Dictionary<string, HttpHeader> headers, string body)
        {
            var result = new Dictionary<string, string>();

            if (headers.ContainsKey(HttpHeader.ContentType)
                && headers[HttpHeader.ContentType].Value == HttpContentType.FormUrlEncoded)
            {
                result = ParseQuery(body);
            }

            return result;
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

        private static Dictionary<string, HttpHeader> ParseHttpHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new Dictionary<string, HttpHeader>();

            foreach (var headerLine in headerLines)
            {
                if (!String.IsNullOrWhiteSpace(headerLine))
                {
                    var headerTokens = headerLine.Split(':');

                    var headerName = headerTokens[0];
                    var headerValue = headerTokens[1].Trim();

                    headerCollection.Add(headerName, new HttpHeader(headerName, headerValue));
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
