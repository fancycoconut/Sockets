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
            using (var client = new HttpClient())
            {
                var request = new HttpRequest(HttpMethod.Get)
                {
                    Uri = new Uri("https://www.google.co.nz/")
                };

                //request.SetHeader("Accept", "*/*");
                request.SetHeader("Connection", "close");

                Console.WriteLine("My request headers:");
                Console.WriteLine(request.BuildRequestHeader());

                var response = await client.Send(request);
                Console.WriteLine("My response:");
                Console.WriteLine(response);
            }

            Console.ReadLine();
        }
    }
}
