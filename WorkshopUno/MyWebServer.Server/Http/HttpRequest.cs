using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.Concurrent;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {
        private static Dictionary<string, HttpSession> Sessions =
            new Dictionary<string, HttpSession>();
        
        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public IReadOnlyDictionary<string, string> Query { get; private set; }

        public IReadOnlyDictionary<string, HttpHeader> Headers { get; private set; }
        
        public IReadOnlyDictionary<string, HttpCookie> Cookies { get; private set; }

        public HttpSession Session { get; private set; }

        public IReadOnlyDictionary<string, string> Form { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var firstLine = lines.First().Split();

            var method = ParseMethod(firstLine[0]);

            var (path, query) = ParseUrl(firstLine[1]);

            var headers = ParseHeaders(lines.Skip(1));

            var cookies = ParseCookies(headers);

            var session = GetSession(cookies);

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(Environment.NewLine, bodyLines);

            var form = ParseForm(headers, body);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headers,
                Cookies = cookies,
                Session = session,
                Body = body,
                Form = form
            };
        }

        private static HttpSession GetSession(Dictionary<string, HttpCookie> cookies)
        {
            var sessionId = cookies.ContainsKey(HttpSession.SessionCookieName)
                ? cookies[HttpSession.SessionCookieName].Value
                : Guid.NewGuid().ToString();

                if (!Sessions.ContainsKey(sessionId))
                {
                    Sessions[sessionId] = new HttpSession(sessionId);
                }

                return Sessions[sessionId];
        }

        private static Dictionary<string, string> ParseForm(Dictionary<string, HttpHeader> headers, string body)
        {
            var result = new Dictionary<string, string>();

            if (headers.ContainsKey(HttpHeader.ContentType)
                && headers[HttpHeader.ContentType].Value == HttpContentType.FormUrlEncoded)
            {
                result = ParseQuery(body);
            }

            return result;
        }

        private static HttpMethod ParseMethod(string method)
        {
            switch (method.ToUpper())
            {
                case "GET": return HttpMethod.Get;
                case "POST": return HttpMethod.Post;
                case "PUT": return HttpMethod.Put;
                case "DELETE": return HttpMethod.Delete;
                default: return HttpMethod.Get; //throw new InvalidOperationException($"Method {method} is not supported on this server");
            }
        }

        private static Dictionary<string, HttpHeader> ParseHeaders(IEnumerable<string> headerLines)
        {
            var headers = new Dictionary<string, HttpHeader>();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var headerTokens = headerLine.Split(new[] { ':' }, 2);

                if (headerTokens.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid.");
                }

                var headerName = headerTokens[0];
                var headerValue = headerTokens[1].Trim();

                headers.Add(headerName, new HttpHeader(headerName, headerValue));
            }

            return headers;
        }

        private static Dictionary<string, HttpCookie> ParseCookies(Dictionary<string, HttpHeader> headers)
        {
            var cookiesCollection = new Dictionary<string, HttpCookie>();

            if (headers.ContainsKey(HttpHeader.Cookie))
            {
                var cookies = headers[HttpHeader.Cookie].Value
                    .Split(new[] { "; " }, StringSplitOptions.None);

                foreach (var cookie in cookies)
                {
                    var cookieItems = cookie.Split('=');

                    var cookieName = cookieItems[0];
                    var cookieValue = cookieItems[1];

                    cookiesCollection.Add(cookieName, new HttpCookie(cookieName, cookieValue));
                }
            }

            return cookiesCollection;
        }

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?');

            var path = urlParts[0];

            var query = urlParts.Length > 1
                ? ParseQuery(urlParts[1])
                : new Dictionary<string, string>();

            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string queryString)
            => queryString
                .Split('&')
                .Select(part => part.Split('='))
                .Where(part => part.Length == 2)
                .ToDictionary(part => part[0], part => part[1]);
    }
}
