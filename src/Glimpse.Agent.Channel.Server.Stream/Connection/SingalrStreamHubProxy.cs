using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Glimpse.Agent.Channel.Server.Stream.Connection
{
    internal class SignalrStreamHubProxy : IStreamHubProxy, IDisposable
    {
        private readonly ConcurrentDictionary<long, Task> _pendingTasks;
        private readonly IHubProxy _hubProxy;
        private long _taskId;

        public SignalrStreamHubProxy(IHubProxy hubProxy)
        {
            _pendingTasks = new ConcurrentDictionary<long, Task>();
            _hubProxy = hubProxy;
        }

        public async Task Invoke(string method, params object[] args)
        {
            var id = Interlocked.Increment(ref _taskId);

            try
            {
                var task = _hubProxy.Invoke(method, args);

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

        public async Task<T> Invoke<T>(string method, params object[] args)
        {
            var id = Interlocked.Increment(ref _taskId);

            try
            {
                var task = _hubProxy.Invoke<T>(method, args);

                _pendingTasks.GetOrAdd(id, _ => task);

                return await task;
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
        }
    }
}