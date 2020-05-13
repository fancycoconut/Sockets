using Sockets.Coap;
using Sockets.Core.Http;
using System;
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
            var request = new CoapRequest(CoapMethod.Get, CoapMessageType.Confirmable)
            {
                Id = 23
            };
            var data = request.Serialize();
            using (var client = new CoapUdpClient())
            {
                var uri = new Uri("coap://localhost:5683/hello");
                var response = await client.Send(uri, data);
            }

            Console.ReadLine();
        }
    }
}
