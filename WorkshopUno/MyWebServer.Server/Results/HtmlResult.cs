using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Results
{
    public class HtmlResult : ContentResult
    {
        public HtmlResult(HttpResponse response, string html) 
            : base(response, html, HttpContentType.Html)
        {
        }
    }
}
