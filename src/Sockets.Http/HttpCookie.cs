using System;
using System.Text.RegularExpressions;

namespace Sockets.Http
{
    public class HttpCookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Expires { get; set; }
        public int MaxAge { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }        
        public bool Secure { get; set; }
        public bool HttpOnly { get; set; }

        public HttpCookie(string cookie)
        {
            ParseCookie(cookie);
        }

        protected void ParseCookie(string cookie)
        {
            var properties = Regex.Split(cookie, "; ");
            var nameValuePair = properties[0].Split('=');
            Name = nameValuePair[0];
            Value = nameValuePair[1];

            foreach (var prop in properties)
            {
                var keyValuePair = prop.Split('=');
                switch (keyValuePair[0].ToLower())
                {
                    case "expires":
                        Expires = keyValuePair[1];
                        break;
                    case "max-age":
                        MaxAge = Convert.ToInt32(keyValuePair[1]);
                        break;
                    case "domain":
                        Domain = keyValuePair[1];
                        break;
                    case "path":
                        Path = keyValuePair[1];
                        break;
                    case "secure":
                        Secure = true;
                        break;
                    case "httponly":
                        HttpOnly = true;
                        break;
                    default:
                        Name = keyValuePair[0];
                        Value = keyValuePair[1];
                        break;
                }
            }
        }       
    }
}
