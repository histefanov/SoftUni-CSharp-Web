using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Identity
{
    public class UserIdentity
    {
        public string Id { get; set; }

        public bool IsAuthenticated => this.Id != null;
    }
}
