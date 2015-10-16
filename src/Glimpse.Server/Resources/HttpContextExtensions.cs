// ReSharper disable RedundantUsingDirective
using System;
// ReSharper restore RedundantUsingDirective
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Resources
{
    public static class HttpContextExtensions
    {
        public async static Task RespondWith(this HttpContext context, IResponse response)
        {
            await response.Respond(context);
        }

        public static IResponse EnableCaching(this IResponse response, TimeSpan? timeSpan = null)
        {
            return new ResponseDecorator(response, ctx =>
            {
#if !DEBUG
                var cacheControl = new CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = timeSpan ?? TimeSpan.FromDays(150)
                };
#else
                var cacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                };
#endif
                ctx.Response.Headers[HeaderNames.CacheControl] = cacheControl.ToString();
            });
        }
    }

    public interface IResponse
    {
        Task Respond(HttpContext context);
    }

    public class RawJson : IResponse
    {
        private readonly string _json;

        public RawJson(string json)
        {
            _json = json;
        }

        public async Task Respond(HttpContext context)
        {
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "application/json";
            await response.WriteAsync(_json);
        }
    }

    public class ResponseDecorator : IResponse
    {
        private readonly IResponse _response;
        private readonly Action<HttpContext> _decoration;
        public ResponseDecorator(IResponse response, Action<HttpContext> decoration)
        {
            _response = response;
            _decoration = decoration;
        }

        public async Task Respond(HttpContext context)
        {
            _decoration(context);
            await _response.Respond(context);
        }
    }
}
