using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }

        public string Url { get; private set; }

        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            string[] lines = request.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None);


        }
    }
}
