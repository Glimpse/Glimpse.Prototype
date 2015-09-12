using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http;
using System;
using System.Linq;
using System.Threading;

namespace Glimpse.Server.Web
{
    public class MessageStreamResource : IResourceStartup
    {
        private readonly IServerBroker _serverBroker;
        private readonly ISubject<string> _senderSubject;
        private readonly SemaphoreSlim _syncLock = new SemaphoreSlim(1);

        public MessageStreamResource(IServerBroker serverBroker)
        {
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
                    // Only 1 thread can access the function or functions that use this lock, 
                    // others trying to access - will wait until the first one released.
                    await _syncLock.WaitAsync();
                    
                    await context.Response.WriteAsync($"data: [{t}]\n\n");
                    await context.Response.Body.FlushAsync();

                    _syncLock.Release();
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
            _senderSubject.OnNext(message.Payload);
        }
    }
}
