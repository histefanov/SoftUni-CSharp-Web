using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Results
{
    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(HttpResponse response) 
            : base(response)
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
