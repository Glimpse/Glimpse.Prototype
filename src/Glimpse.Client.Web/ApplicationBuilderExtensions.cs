
using System.Reflection;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;

namespace Microsoft.AspNet.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlimpseUI(this IApplicationBuilder app)
        {
            var options = new FileServerOptions();
            options.RequestPath = "/GlimpseUI";
            options.EnableDefaultFiles = false;
            options.StaticFileOptions.ContentTypeProvider = new FileExtensionContentTypeProvider();
            options.FileProvider = new EmbeddedFileProvider(
                typeof(ApplicationBuilderExtensions).GetTypeInfo().Assembly,
                "Glimpse.Client.Web.Resources");

            app.UseFileServer(options);

            return app;
        }
    }
}
