﻿using MyWebServer.Server.Common;

namespace MyWebServer.Server.Http
{
    public class HttpCookie
    {
        public HttpCookie(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public override string ToString()
            => $"{this.Name}={this.Value}";
    }
}
