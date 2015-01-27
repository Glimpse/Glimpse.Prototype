using Glimpse.Web;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public interface IRequestProfiler
    {
        Task Begin(IHttpContext newContext);

        Task End(IHttpContext newContext);
    }
}