using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Glimpse.Server.Internal.Resources
{
    public class MessageIngressResource : IResource
    {
        private readonly IServerBroker _messageServerBus;

        public MessageIngressResource(IServerBroker messageServerBus)
        {
            _messageServerBus = messageServerBus;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            if (context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                // TODO: Wrap in a try block and return Http Problem when required
                var messages = ReadMessage(context.Request).ToArray();
                foreach (var message in messages)
                {
                    _messageServerBus.SendMessage(message);
                }

                await context.RespondWith(
                    new HttpStatusResponse(HttpStatusCode.Accepted, 
                    "Accepted: " + string.Join(", ", messages.Select(m => m.Id.ToString("N")))));
            }
            else
            {
                await context.RespondWith(
                    new InvalidMethodProblem(context.Request.Method, "POST"));
            }
        }

        public string Name => "message-ingress";

        public IEnumerable<ResourceParameter> Parameters => Enumerable.Empty<ResourceParameter>();

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