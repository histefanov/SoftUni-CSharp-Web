using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyWebServer.Server.Responses
{
    public class ViewResponse : HttpResponse
    {
        public ViewResponse(string viewPath) 
            : base(HttpStatusCode.OK)
        {
            this.GetHtml(viewPath);
        }

        private void GetHtml(string viewPath)
        {
            viewPath = Path.GetFullPath("./Views/" + viewPath.TrimStart('/') + ".cshtml");

            if (!File.Exists(viewPath))
            {
                this.StatusCode = HttpStatusCode.NotFound;
                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            this.PrepareContent(viewContent, HttpContentType.Html);
        }
    }
}
