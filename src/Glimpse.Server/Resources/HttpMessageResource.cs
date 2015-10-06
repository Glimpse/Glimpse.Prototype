using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Glimpse.Server.Web;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Glimpse.Server.Resources
{
    public class HttpMessageResource : IResource
    {
        private readonly IServerBroker _messageServerBus;

        public HttpMessageResource(IServerBroker messageServerBus)
        {
            _messageServerBus = messageServerBus;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var messages = ReadMessage(context.Request);
            foreach (var message in messages)
            {
                _messageServerBus.SendMessage(message);
            }

            // TODO: Really should do something better
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "text/plain";

            var data = Encoding.UTF8.GetBytes("OK");
            await response.Body.WriteAsync(data, 0, data.Length);
        }

        public string Name => "AgentMessage";

        public ResourceParameters Parameters => null;

        public ResourceType Type => ResourceType.Agent;

        private IEnumerable<Message> ReadMessage(HttpRequest request)
        {
            var reader = new StreamReader(request.Body);
            var text = reader.ReadToEnd();

            var messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(text);

            return messages;
        }
    }
}