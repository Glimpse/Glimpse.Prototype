using System;
using System.Collections.Generic;

namespace Glimpse.Web
{
    public interface IMiddlewareLogicComposerProvider
    {
        IEnumerable<IMiddlewareLogicComposer> Logic { get; }
    }
}