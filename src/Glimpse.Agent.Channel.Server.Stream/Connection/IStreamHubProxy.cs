using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Channel.Server.Stream.Connection
{
    public interface IStreamHubProxy
    {
        Task Invoke(string method, params object[] args);

        Task<TResult> Invoke<TResult>(string method, params object[] args);
    }
}