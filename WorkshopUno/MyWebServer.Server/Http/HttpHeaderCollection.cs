using System;
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

        public HttpHeader this[string name]
            => this.headers[name];

        public void Add(string name, string value)
        {
            this.headers.Add(name, new HttpHeader(name, value));
        }

        public bool Contains(string name)
            => this.headers.ContainsKey(name);

        public HttpHeader Get(string name)
        {
            if (!this.Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return this.headers[name];
        }

        public IEnumerator<HttpHeader> GetEnumerator()
            => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}