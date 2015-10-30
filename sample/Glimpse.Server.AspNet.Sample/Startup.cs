using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.Server.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlimpse();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpseServer();

            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(
@"
<html>
<head><title>Welcome to Glimpse Server!</title></head>
<body>
<h1><img src='http://getglimpse.com/content/glimpse100.png' style='vertical-align: middle;'> Glimpse Server</h1>
<p><a href='/glimpse/export-config'>Download configuration.</a></p>
</body>
</html>
");
            });
        }
    }
}
