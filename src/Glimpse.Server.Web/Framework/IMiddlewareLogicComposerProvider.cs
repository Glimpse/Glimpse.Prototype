using System.Collections.Generic;

namespace Glimpse.Server.Web
{
    public interface IMiddlewareLogicComposerProvider
    {
        IEnumerable<IMiddlewareLogicComposer> Logic { get; }
    }
}