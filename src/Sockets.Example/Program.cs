using Sockets.Coap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MainAsync(args)
                .GetAwaiter()
                .GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            //using (var client = new HttpClient())
            //{
            //    var request = new HttpRequest(HttpMethod.Get)
            //    {
            //        Uri = new Uri("https://www.google.co.nz/")
            //    };

            //    //request.SetHeader("Accept", "*/*");
            //    request.SetHeader("Connection", "close");

            //    Console.WriteLine("My request headers:");
            //    Console.WriteLine(request.BuildRequestHeader());

            //    var response = await client.Send(request);
            //    Console.WriteLine("My response:");
            //    Console.WriteLine(response);
            //}


            // Test CoAP
            var request = new CoapRequest(CoapMethod.Post, CoapMessageType.Confirmable)
            {
                Id = 23,
                Options = new List<CoapOption>
                {
                    new CoapOption(Option.UriPath, "storage"),
                    new CoapOption(Option.ContentFormat, (int) MediaType.TextPlain)
                },
                Payload = Encoding.UTF8.GetBytes("data")
            };


            var data = request.Serialize();
            using (var client = new CoapUdpClient())
            {
                var getResponse = await client.GetConfirmable(new Uri("coap://localhost:5683/hello"));

                var uri = new Uri("coap://localhost:5683");
                var response = await client.Send(uri, data);
            }

            

            Console.ReadLine();
        }
    }
}
