using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Sockets.Http.Tests.Helpers
{
    public class ResourceHelper
    {
        public static Task<string> GetResponse(string name)
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(baseDir, $"Resources\\Responses\\{name}.txt");

            return File.ReadAllTextAsync(path);
        }
    }
}
