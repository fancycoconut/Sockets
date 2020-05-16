using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Sockets.Http.Content
{
    public class TextContent : IHttpContent
    {
        public string Body { get; set; }

        public static TextContent FromChunkedText(string response)
        {
            var sb = new StringBuilder();
            var chunks = Regex.Split(response, Constants.CRLF);

            var length = 0;
            //for (var i = 0;)

           // var length = response.IndexOf("\")

            throw new NotImplementedException();
        }

        //private readonly static contentTypes = new Di 
    }
}
