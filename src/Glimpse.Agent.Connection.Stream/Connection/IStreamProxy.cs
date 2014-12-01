using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Connection.Stream.Connection
{
    public interface IStreamProxy
    {
        Task<TResult> UseSender<TResult>(Func<IStreamInvokerProxy, Task<TResult>> callback);

        Task UseSender(Func<IStreamInvokerProxy, Task> callback);
    }
}