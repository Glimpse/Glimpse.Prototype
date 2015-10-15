using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Glimpse.Server.Internal.Extensions;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;

namespace Glimpse.Server.Internal.Resources
{
    public class MessageStreamResource : IResourceStartup
    {
        private readonly IServerBroker _serverBroker;
        private readonly ISubject<Container> _senderSubject;
        private readonly SemaphoreSlim _syncLock = new SemaphoreSlim(1);

        public MessageStreamResource(IServerBroker serverBroker)
        {
            _serverBroker = serverBroker;

            _senderSubject = new Subject<Container>();

            // setup heart beat to keep alive client connections
            Observable.Interval(new TimeSpan(0, 0, 20), TaskPoolScheduler.Default).Subscribe(x => _senderSubject.OnNext(Container.Ping()));
            
            // TODO: See if we can get Defered working there
            // lets subscribe, hope of the thread and then broadcast to all connections
            //_serverBroker.OffRecieverThread.ListenAll().Subscribe(message => Observable.Defer(() => Observable.Start(() => ProcessMessage(message), TaskPoolScheduler.Default)));
            _serverBroker.OffRecieverThread.ListenAll().Subscribe(message => Observable.Start(() => ProcessMessage(message), TaskPoolScheduler.Default));
        }

        public void Configure(IResourceBuilder resourceBuilder)
        {
            resourceBuilder.Run("message-stream", "{?types}", ResourceType.Client, async (context, parameters) =>
            {
                var continueTask = new TaskCompletionSource<bool>();

                // Disable request compression
                var buffering = context.Features.Get<IHttpBufferingFeature>();
                if (buffering != null)
                {
                    buffering.DisableRequestBuffering();
                }

                // Filter when types are present
                var types = parameters.ParseEnumerable("types").ToArray();
                Func<Container, bool> filter = c => true;
                if (types.Length > 0)
                    filter = c => c.Types.Intersect(types).Any();

                context.Response.ContentType = "text/event-stream";
                await context.Response.WriteAsync("retry: 5000\n\n");
                await context.Response.Body.FlushAsync();

                var unSubscribe = _senderSubject.Where(filter).Subscribe(async t =>
                {
                    await _syncLock.WaitAsync();

                    try
                    {
                        await context.Response.WriteAsync($"event: {t.Event}\n");
                        await context.Response.WriteAsync($"data: [{t.Data}]\n\n");
                        await context.Response.Body.FlushAsync();
                    }
                    finally 
                    {
                        _syncLock.Release();
                    }
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
            _senderSubject.OnNext(Container.Message(message.Types, message.Payload));
        }

        private class Container
        {
            private Container(string @event, IEnumerable<string> types, string data)
            {
                Event = @event;
                Data = data;
                Types = types;
            }

            public static Container Ping()
            {
                return new Container("ping", Enumerable.Empty<string>(), "");
            }

            public static Container Message(IEnumerable<string> types, string data)
            {
                return new Container("message", types, data);
            }

            public string Data { get; }

            public IEnumerable<string> Types { get; }

            public string Event { get; }
        }
    }
}
