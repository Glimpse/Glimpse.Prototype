using System.Reflection;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Internal.Resources
{
    public class EmbeddedFileResource : IResourceStartup
    {
        public void Configure(IResourceBuilder resourceBuilder)
        {
            UseCaching(resourceBuilder);

            resourceBuilder.AppBuilder.UseFileServer(new FileServerOptions
            {
                RequestPath = "",
                EnableDefaultFiles = true,
                FileProvider =
                    new EmbeddedFileProvider(typeof (EmbeddedFileResource).GetTypeInfo().Assembly,
                        "Glimpse.Server.Internal.Resources.Embeded")
            });

            resourceBuilder.RegisterResource("Client", "index.html?hash={hash}{&requestId}");
            resourceBuilder.RegisterResource("Diagnostics", "index.html?hash={hash}{&requestId}");
            resourceBuilder.RegisterResource("HudClient", "hud.js?hash={hash}");
            // TODO: This is an "agent script", not a client script
            resourceBuilder.RegisterResource("BrowserAgent", "scripts/BrowserAgent.js?hash={hash}");
        }

        private static void UseCaching(IResourceBuilder resourceBuilder)
        {
#if !DEBUG
            var cacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(150)
            };
#else
            var cacheControl = new CacheControlHeaderValue
            {
                NoCache = true,
            };
#endif
            resourceBuilder.AppBuilder.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add(HeaderNames.CacheControl, cacheControl.ToString());
                await next();
            });
        }

        public ResourceType Type => ResourceType.Client;
    }
}