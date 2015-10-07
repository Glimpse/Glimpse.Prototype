using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Resources
{
    public class HelloWorldResource : IResource
    {
        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;

            response.Headers[HeaderNames.ContentType] = "text/plain";

            var data = Encoding.UTF8.GetBytes("Hello world, Glimpse!");
            await response.Body.WriteAsync(data, 0, data.Length);
        }

        public string Name => "HelloWorld";

        public IEnumerable<ResourceParameter> Parameters => Enumerable.Empty<ResourceParameter>();

        public ResourceType Type => ResourceType.Client;
    }
}