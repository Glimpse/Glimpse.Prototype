using Glimpse.Web;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public interface IRequestProfiler
    {
        void Begin(IHttpContext newContext);

        void End(IHttpContext newContext);
    }
}