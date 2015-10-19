// ReSharper disable RedundantUsingDirective
using System;
// ReSharper restore RedundantUsingDirective
using System.Threading.Tasks;
using Glimpse.Server.Internal.Resources;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Resources
{
    public static class HttpContextExtensions
    {
        public static IApplicationBuilder ModifyResponseWith(this IApplicationBuilder builder, Func<IResponse, IResponse> useFunc)
        {
            var nullImpl = new NullResponse();
            var response = useFunc(nullImpl);

            builder.Use(async (ctx, next) =>
            {
                await response.Respond(ctx);
                await next();
            });

            return builder;
        }

        public async static Task RespondWith(this HttpContext context, IResponse response)
        {
            await response.Respond(context);
        }

        public static IResponse AsFile(this IResponse response, string filename)
        {
            return new ResponseDecorator(response, ctx => ctx.Response.Headers[HeaderNames.ContentDisposition] = $"attachment; filename = ${filename}");
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
}
