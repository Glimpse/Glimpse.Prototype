using System;

namespace Glimpse.Web
{
    public static class HttpRequestExtension
    {
        public static string Uri(this IHttpRequest request)
        { 
            return $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}";
        }
        public static string UriAbsolute(this IHttpRequest request)
        {
            return $"{request.Path}{request.QueryString}";
        }
    }
}