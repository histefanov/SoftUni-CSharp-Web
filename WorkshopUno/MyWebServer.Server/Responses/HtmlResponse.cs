using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text) 
            : base(text, "text/html; charset=UTF-8")
        {
        }
    }
}
