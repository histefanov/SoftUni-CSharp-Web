using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyWebServer.Server
{
    public abstract class Controller
    {
        protected Controller(HttpRequest request)
            => this.Request = request;

        protected HttpRequest Request { get; private set; }

        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string html)
            => new HtmlResponse(html);

        protected HttpResponse Redirect(string location)
            => new RedirectResponse(location);

        protected HttpResponse View([CallerMemberName] string viewName = "")
            => new ViewResponse(viewName, this.GetControllerName());

        private string GetControllerName()
            => this.GetType().Name.Replace(nameof(Controller), string.Empty);
    }
}
