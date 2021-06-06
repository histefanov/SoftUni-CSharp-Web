using System.Collections;
using System.Collections.Generic;

namespace MyWebServer.Server.Http
{
    public class HttpHeaderCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public int Count => this.headers.Count;

        public void Add(string name, string value)
        {
            this.headers.Add(name, new HttpHeader(name, value));
        }

        public IEnumerator<HttpHeader> GetEnumerator()
            => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}