using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Responses
{
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse() 
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
