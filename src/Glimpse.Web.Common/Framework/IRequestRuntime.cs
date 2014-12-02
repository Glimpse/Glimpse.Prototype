using System;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public interface IRequestRuntime
    {
        Task Begin(IContext newContext);

        Task End(IContext newContext);
    }
}