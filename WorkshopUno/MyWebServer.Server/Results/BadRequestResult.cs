using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Results
{
    public class BadRequestResult : HttpResponse
    {
        public BadRequestResult() 
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
