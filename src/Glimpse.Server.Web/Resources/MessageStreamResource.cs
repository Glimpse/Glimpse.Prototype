using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Glimpse.Server.Web
{
    public class MessageStreamResource : IResourceStartup
    {
        private readonly IServerBroker _serverBroker;
        private readonly ISubject<string> _senderSubject;
        private readonly JsonSerializer _jsonSerializer;

        public MessageStreamResource(IServerBroker serverBroker, JsonSerializer jsonSerializer)
        {
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            _jsonSerializer = jsonSerializer;
            _serverBroker = serverBroker;

            _senderSubject = new Subject<string>();

            // TODO: See if we can get Defered working there
            // lets subscribe, hope of the thread and then broadcast to all connections
            //_serverBroker.OffRecieverThread.ListenAll().Subscribe(message => Observable.Defer(() => Observable.Start(() => ProcessMessage(message), TaskPoolScheduler.Default)));
            _serverBroker.OffRecieverThread.ListenAll().Subscribe(message => Observable.Start(() => ProcessMessage(message), TaskPoolScheduler.Default));
        }

        public void Configure(IResourceBuilder resourceBuilder)
        {
            resourceBuilder.Run("MessageStream", null, ResourceType.Client, async (context, dictionary) =>
            {
                var continueTask = new TaskCompletionSource<bool>();

                // Disable request compression
                var buffering = context.Features.Get<IHttpBufferingFeature>();
                if (buffering != null)
                {
                    buffering.DisableRequestBuffering();
                }

                context.Response.ContentType = "text/event-stream";
                await context.Response.WriteAsync("retry: 5000\n\n");
                //await context.Response.WriteAsync("data: pong\n\n");
                await context.Response.Body.FlushAsync();

                var unSubscribe = _senderSubject.Subscribe(async t =>
                {
                    // TODO: its possible to get multiple writes happen at once here,
                    //       need to figure out how to prevent that.
                    await context.Response.WriteAsync($"data: {t}\n\n");
                    await context.Response.Body.FlushAsync();
                });

                context.RequestAborted.Register(() =>
                {
                    continueTask.SetResult(true);
                    unSubscribe.Dispose();
                });

                await continueTask.Task;
            });
        }

        public ResourceType Type => ResourceType.Client;

        private void ProcessMessage(IMessage message)
        {
            var payload = _jsonSerializer.Serialize(message);

            _senderSubject.OnNext(payload);
        }
    }
}
