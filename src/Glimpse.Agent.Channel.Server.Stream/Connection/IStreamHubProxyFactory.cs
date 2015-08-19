using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Channel.Server.Stream.Connection
{
    public interface IStreamHubProxyFactory
    {
        void Register(string hubName, Action<IStreamHubProxy> proxyCallback);

        Task Start();
    }
}