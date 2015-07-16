using System;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public interface IRequestRuntime
    {
        void Begin(IHttpContext newContext);

        void End(IHttpContext newContext);
    }
}