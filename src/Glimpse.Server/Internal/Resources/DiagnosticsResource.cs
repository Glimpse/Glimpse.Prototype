using System.Reflection;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;

namespace Glimpse.Server.Internal.Resources
{
    public class DiagnosticsResource : IResourceStartup
    {
        public void Configure(IResourceBuilder resourceBuilder)
        {
            var options = new FileServerOptions
            {
                RequestPath = "/Diagnostics",
                EnableDefaultFiles = false
            };
            options.StaticFileOptions.ContentTypeProvider = new FileExtensionContentTypeProvider();
            options.FileProvider = new EmbeddedFileProvider(GetType().GetTypeInfo().Assembly, "Glimpse.Server.Internal.Resources.Embeded.Diagnostics");

            resourceBuilder.AppBuilder.UseFileServer(options);
        }

        public ResourceType Type => ResourceType.Client;
    }
}
