using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Connection.Stream.Connection
{
    public interface IStreamHubProxyFactory
    {
        void Register(string hubName, Action<IStreamHubProxy> proxyCallback);

        Task Start();
    }
}