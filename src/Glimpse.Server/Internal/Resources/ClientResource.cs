using System.Reflection;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;

namespace Glimpse.Server.Internal.Resources
{
    public class ClientResource : IResourceStartup
    {
        public void Configure(IResourceBuilder resourceBuilder)
        {
            // TODO: Add HTTP Caching
            var options = new FileServerOptions();
            options.RequestPath = "/Client";
            options.EnableDefaultFiles = false;
            options.StaticFileOptions.ContentTypeProvider = new FileExtensionContentTypeProvider();
            options.FileProvider = new EmbeddedFileProvider(typeof(ClientResource).GetTypeInfo().Assembly, "Glimpse.Server.Internal.Resources.Embeded.Client");

            resourceBuilder.AppBuilder.UseFileServer(options);
        }

        public ResourceType Type => ResourceType.Client;
    }
}