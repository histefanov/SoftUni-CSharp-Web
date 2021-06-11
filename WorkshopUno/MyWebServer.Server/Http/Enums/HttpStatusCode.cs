using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Http
{
    public enum HttpStatusCode
    {
        OK = 200,
        BadRequest = 400,
        Found = 302,
        NotFound = 404
    }
}
