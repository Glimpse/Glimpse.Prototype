using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Connection.Stream.Connection
{
    public class StreamInvokerProxy : IStreamInvokerProxy
    {
        private readonly IHubProxy _hubProxy;

        internal StreamInvokerProxy(IHubProxy hubProxy)
        {
            _hubProxy = hubProxy;
        }

        public Task Invoke(string method, params object[] args)
        {
            return _hubProxy.Invoke(method, args);
        }

        public Task<T> Invoke<T>(string method, params object[] args)
        {
            return _hubProxy.Invoke<T>(method, args);
        }
    }
}