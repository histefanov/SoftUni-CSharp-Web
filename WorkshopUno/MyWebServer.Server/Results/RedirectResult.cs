using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Results
{
    public class RedirectResult : ActionResult
    {
        public RedirectResult(HttpResponse response, string location)    
            : base(response)
        {
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader(HttpHeader.Location, location);
        }
    }
}
