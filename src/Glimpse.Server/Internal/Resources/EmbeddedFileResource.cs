using System.Collections.Generic;
using System.Reflection;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Internal.Resources
{
    public abstract class EmbeddedFileResource : IResourceStartup
    {
        public void Configure(IResourceBuilder resourceBuilder)
        {
            UseCaching(resourceBuilder);

            resourceBuilder.AppBuilder.UseFileServer(new FileServerOptions
            {
                RequestPath = "",
                EnableDefaultFiles = true,
                FileProvider =
                    new EmbeddedFileProvider(typeof (EmbeddedFileResource).GetTypeInfo().Assembly, BaseNamespace)
            });

            foreach (var registration in Register)
            {
                resourceBuilder.RegisterResource(registration.Key, registration.Value);
            }
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
                ctx.Response.Headers[HeaderNames.CacheControl] = cacheControl.ToString();
                await next();
            });
        }

        public abstract ResourceType Type { get; }

        public abstract string BaseNamespace { get; }

        public abstract IDictionary<string, string> Register { get; }
    }

    public class ClientEmbeddedFileResource : EmbeddedFileResource
    {
        public override ResourceType Type => ResourceType.Client;

        public override string BaseNamespace => "Glimpse.Server.Internal.Resources.Embeded.Client";

        public override IDictionary<string, string> Register => new Dictionary<string, string>
        {
            { "client", "index.html?hash={hash}{&requestId}"},
            { "diagnostics", "index.html?hash={hash}{&requestId}" },
            { "hud", "hud.js?hash={hash}" }
        };
    }

    public class AgentEmbeddedFileResource : EmbeddedFileResource
    {
        public override ResourceType Type => ResourceType.Agent;

        public override string BaseNamespace => "Glimpse.Server.Internal.Resources.Embeded.Agent";

        public override IDictionary<string, string> Register => new Dictionary<string, string>
        {
            { "agent", "agent.js?hash={hash}"}
        };
    }
}