using MyWebServer.Server.Http;
using System.Collections.Generic;

namespace MyWebServer.Server.Results
{
    public class ActionResult : HttpResponse
    {
        protected ActionResult(
            HttpResponse response) 
            : base(response.StatusCode)
        {
            this.Content = response.Content;
            this.PrepareHeaders(response.Headers);
            this.PrepareCookies(response.Cookies);
        }

        private void PrepareHeaders(IDictionary<string, HttpHeader> headers)
        {
            foreach (var header in headers)
            {
                this.Headers.Add(header.Key, header.Value);
            }
        }

        private void PrepareCookies(IDictionary<string, HttpCookie> cookies)
        {
            foreach (var cookie in cookies)
            {
                this.Cookies.Add(cookie.Key, cookie.Value);
            }
        }
    }
}
