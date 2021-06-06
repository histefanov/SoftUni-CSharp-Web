using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Common
{
    public static class Guard
    {
        public static void AgainstNull(object value, string name = null)
        {
            if (value == null)
            {
                if (String.IsNullOrEmpty(name)) //Empty?
                {
                    name = "Value";
                }

                throw new ArgumentException($"{name} cannot be null.");
            }
        }
    }
}
