using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Glimpse.Server.Web
{
    public class AgentMessageResource : IResource
    {
        private readonly IServerBroker _messageServerBus;

        public AgentMessageResource(IServerBroker messageServerBus)
        {
            _messageServerBus = messageServerBus;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var envelope = ReadMessage(context.Request);

            _messageServerBus.SendMessage(envelope);

            // TODO: Really should do something better
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "text/plain";

            var data = Encoding.UTF8.GetBytes("OK");
            await response.Body.WriteAsync(data, 0, data.Length);
        }

        public string Name => "AgentMessage";

        public ResourceParameters Parameters => null;

        private Message ReadMessage(HttpRequest request)
        {
            var reader = new StreamReader(request.Body);
            var text = reader.ReadToEnd();

            var message = JsonConvert.DeserializeObject<Message>(text);

            return message;
        }
    }
}