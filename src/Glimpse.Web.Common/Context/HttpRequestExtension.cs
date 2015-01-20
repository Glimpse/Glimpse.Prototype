using System;

namespace Glimpse.Web
{
    public static class HttpRequestExtension
    {
        public static string Uri(this IHttpRequest request)
        { 
            return "${request.Scheme}://${request.Host}${request.PathBase}${request.Path}${request.QueryString}";
        }
    }
}