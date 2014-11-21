using System;

namespace Glimpse.Web
{
    public interface IRequestRuntime
    {
        void Begin(IContext newContext);

        void End(IContext newContext);
    }
}