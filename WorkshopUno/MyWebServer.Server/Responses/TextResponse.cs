using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Responses
{
    public class TextResponse : HttpResponse
    {
        public TextResponse(HttpStatusCode statusCode)
            : base(HttpStatusCode.OK)
        {
        }
    }
}
