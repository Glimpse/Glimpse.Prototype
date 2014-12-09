using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Glimpse.Agent.Connection.Stream.Connection
{
    public class DefaultStreamProxy : IStreamProxy, IDisposable
    {
        private readonly ConcurrentDictionary<long, Task> _pendingTasks;
        private readonly Lazy<Task<IHubProxy>> _hub;
        private HubConnection _connection;
        private long _taskId;

        public DefaultStreamProxy()
        {
            _pendingTasks = new ConcurrentDictionary<long, Task>();
            _hub = new Lazy<Task<IHubProxy>>(() => GetApplicationHubProxy());
        }
         
        public async Task<TResult> UseSender<TResult>(Func<IStreamInvokerProxy, Task<TResult>> callback)
        {
            long id = Interlocked.Increment(ref _taskId);

            try
            {
                var hub = await _hub.Value;  
                var task = callback(new StreamInvokerProxy(hub));

                _pendingTasks.GetOrAdd(id, _ => task);

                return await task;
            }
            finally
            {
                Task removed;
                _pendingTasks.TryRemove(id, out removed);
            }
        }

        public async Task UseSender(Func<IStreamInvokerProxy, Task> callback)
        {
            long id = Interlocked.Increment(ref _taskId);

            try
            {
                var hub = await _hub.Value;
                var task = callback(new StreamInvokerProxy(hub));

                _pendingTasks.GetOrAdd(id, _ => task);

                await task;
            }
            catch (Exception e)
            { 
                await new Task(() => { });
            }
            finally
            {
                Task removed;
                _pendingTasks.TryRemove(id, out removed);
            } 
        }

        public void Dispose()
        {
            Task.WaitAll(_pendingTasks.Values.ToArray(), TimeSpan.FromSeconds(60));

            if (_connection != null)
            {
                _connection.Dispose();
            }
        }

        private async Task<IHubProxy> GetApplicationHubProxy()
        {
            try
            {
                // TODO: Needs to be abstracted out or pushed out and made more generic
                var hubConnection = new HubConnection("http://localhost:15999/glimpse/stream");

                var applicationHub = hubConnection.CreateHubProxy("RemoteStreamMessagePublisherResource");

                await hubConnection.Start();

                _connection = hubConnection;

                return applicationHub;
            }
            catch (Exception e)
            {
                // TODO: Need to do something with the exeception
                return null;
            }
        }
    }
}