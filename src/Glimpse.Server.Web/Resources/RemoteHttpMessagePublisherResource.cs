using Glimpse.Web;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Server.Resources
{
    public class RemoteHttpMessagePublisherResource : IRequestHandler
    {
        private readonly IMessageServerBus _messageServerBus;

        public RemoteHttpMessagePublisherResource(IMessageServerBus messageServerBus)
        {
            _messageServerBus = messageServerBus;
        }

        public bool WillHandle(IHttpContext context)
        {
            return context.Request.Path.StartsWith("/Glimpse/Agent");
        }

        public async Task Handle(IHttpContext context)
        {
            var envelope = ReadMessage(context.Request);

            _messageServerBus.SendMessage(envelope);

            // TEST CODE ONLY!!!!
            var response = context.Response;

            response.SetHeader("Content-Type", "text/plain");

            var data = Encoding.UTF8.GetBytes(envelope.Payload);
            await response.WriteAsync(data);
            // TEST CODE ONLY!!!!
        }

        private MessageEnvelope ReadMessage(IHttpRequest request)
        {
            var reader = new StreamReader(request.Body);
            var text = reader.ReadToEnd();

            var message = JsonConvert.DeserializeObject<MessageEnvelope>(text);

            return message;
        }
    }
}