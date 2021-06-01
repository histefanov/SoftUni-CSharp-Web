using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }

        public string Url { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            string[] lines = request.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None);

            var firstLine = lines[0].Split();

            var method = ParseHttpMethod(firstLine[0]);
            var url = firstLine[1];
            var headerCollection = ParseHttpHeaderCollection(lines.Skip(1));

            var bodyLines = lines.Skip(headerCollection.Count + 2).ToArray();

            var body = string.Join(Environment.NewLine, bodyLines);

            return new HttpRequest
            {
                Method = method,
                Url = url,
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
                if (headerLine != Environment.NewLine)
                {
                    var headerTokens = headerLine.Split
                        (new[] { ": " },
                        StringSplitOptions.None);

                    var header = new HttpHeader
                    {
                        Name = headerTokens[0],
                        Value = headerTokens[1].Trim()
                    };

                    headerCollection.Add(header);
                }
            }

            return headerCollection;
        }
    }
}
