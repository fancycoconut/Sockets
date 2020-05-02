using Sockets.Core.Http;
using System;
using System.Runtime.InteropServices;
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
            using (var client = new Client())
            {
                var request = new Request(Method.Get)
                {
                    Uri = new Uri("https://www.google.co.nz/")
                };

                request.SetHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.SetHeader("Accept-Encoding", "gzip, deflate, br");
                request.SetHeader("Cache-Control", "no-cache");                
                request.SetHeader("User-Agent", "TestClient");
                request.SetHeader("Connection", "close");

                Console.WriteLine("My request headers:");
                Console.WriteLine(request.BuildRequestHeader());

                var response = await client.Send(request);
                Console.WriteLine("My response:");
            }

            Console.ReadLine();
        }
    }
}
