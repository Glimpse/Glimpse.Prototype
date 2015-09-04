using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Web
{
    public class MessageHistoryResource : IResource
    {
        private readonly InMemoryStorage _store;

        public MessageHistoryResource(IStorage storage)
        {
            // TODO: Really shouldn't be here 
            _store = (InMemoryStorage)storage;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;

            response.Headers[HeaderNames.ContentType] = "application/json";

            var list = await _store.Query(null);

            var sb = new StringBuilder("[");
            sb.Append(string.Join(",", list));
            sb.Append("]");
            var output = sb.ToString();

            await response.WriteAsync(output);
        }

        public string Name => "MessageHistory";

        public ResourceParameters Parameters => null;
    }
}