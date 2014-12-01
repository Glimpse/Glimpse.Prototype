using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Connection.Stream.Connection
{
    public interface IStreamInvokerProxy
    {
        Task Invoke(string method, params object[] args);

        Task<T> Invoke<T>(string method, params object[] args);
    }
}