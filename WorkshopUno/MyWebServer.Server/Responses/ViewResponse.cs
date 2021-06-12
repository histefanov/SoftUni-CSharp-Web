using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyWebServer.Server.Responses
{
    public class ViewResponse : HttpResponse
    {
        private const char PathSeparator = '/';

        public ViewResponse(string viewName, string controllerName) 
            : base(HttpStatusCode.OK)
        {
            this.GetHtml(viewName, controllerName);
        }

        private void GetHtml(string viewName, string controllerName)
        {
            if (!viewName.Contains(PathSeparator.ToString()))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart(PathSeparator) + ".cshtml");

            if (!File.Exists(viewPath))
            {
                PrepareMissingViewError(viewPath);
            }

            var viewContent = File.ReadAllText(viewPath);

            this.PrepareContent(viewContent, HttpContentType.Html);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;

            var errorMessage = $"View '{viewPath}' was not found.";

            this.PrepareContent(errorMessage, HttpContentType.TextPlain);

            return;
        }
    }
}
