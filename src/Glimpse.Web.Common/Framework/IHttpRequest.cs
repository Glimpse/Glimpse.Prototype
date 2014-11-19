using System;
using System.Collections.Generic;

namespace Glimpse.Web
{ 
    public interface IHttpRequest
    {
        string Accept { get; }

        string Method { get; }

        string RemoteIpAddress { get; }

        int? RemotePort { get; }

        string Scheme { get; }

        string Host { get; }

        string PathBase { get; }

        string Path { get; }

        string QueryString { get; }

        string GetQueryString(string key);

        IList<string> GetQueryStringValues(string key);

        string GetHeader(string key);

        IList<string> GetHeaderValues(string key);

        IEnumerable<string> HeaderKeys { get; }

        string GetCookie(string key);

        IList<string> GetCookieValues(string key);

        IEnumerable<string> CookieKeys { get; }
    } 
}