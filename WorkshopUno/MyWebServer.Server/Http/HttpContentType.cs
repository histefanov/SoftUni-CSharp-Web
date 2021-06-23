using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Http
{
    public class HttpContentType
    {
        public const string TextPlain = "text/plain; charset=UTF-8";
        public const string Html = "text/html; charset=UTF-8";
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        public static string GetByFileExtension(string fileExtension)
            => fileExtension switch
            {
                "css" => "text/css",
                "js" => "application/javascript",
                "jpg" => "image/jpg",
                "jpeg" => "image/jpg",
                "png" => "image/png",
                _ => TextPlain
            };
    }
}
