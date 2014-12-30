using System;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public interface IRequestRuntime
    {
        Task Begin(IHttpContext newContext);

        Task End(IHttpContext newContext);
    }
}