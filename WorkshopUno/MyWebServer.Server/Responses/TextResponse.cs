using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using System;
namespace MyWebServer.Server.Responses
{
    public class TextResponse : ContentResponse
    {        
        public TextResponse(string text)
            : base(text, "text/plain; charset=UTF-8")
        {
        }
    }
}
