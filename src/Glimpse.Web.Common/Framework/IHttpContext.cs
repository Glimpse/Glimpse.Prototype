using System;
using System.Collections.Generic;

namespace Glimpse.Web
{
    public interface IHttpContext
    {
        IHttpRequest Request { get; }

        IHttpResponse Response { get; }

        IDictionary<string, object> Items { get; }

        IServiceProvider ApplicationServices { get; }
    }
}