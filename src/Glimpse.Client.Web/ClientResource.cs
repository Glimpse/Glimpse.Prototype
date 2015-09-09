using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;
using Glimpse.Server.Web;

namespace Glimpse.Client.Web
{
    public class ClientResource : IResourceStartup
    {
        public void Configure(IResourceBuilder resourceBuilder)
        {
            var options = new FileServerOptions();
            options.RequestPath = "/Client";
            options.EnableDefaultFiles = false;
            options.StaticFileOptions.ContentTypeProvider = new FileExtensionContentTypeProvider();
            options.FileProvider = new EmbeddedFileProvider(typeof(ClientResource).GetTypeInfo().Assembly, "Glimpse.Client.Web.Resources");

            resourceBuilder.AppBuilder.UseFileServer(options);
        }

        public ResourceType Type => ResourceType.Client;
    }
}
