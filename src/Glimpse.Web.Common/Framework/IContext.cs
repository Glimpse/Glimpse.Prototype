using System;
using System.Collections.Generic;

namespace Glimpse.Web
{
    public interface IContext
    {
        IHttpRequest Request { get; }

        IHttpResponse Response { get; }

        IDictionary<string, object> Items { get; }

        IServiceProvider ApplicationServices { get; }
    }
}