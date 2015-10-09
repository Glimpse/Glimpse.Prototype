using Glimpse.Agent;
using Glimpse.Initialization;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.Agent.Sample
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            /* Example of how to use fixed provider

            TODO: This should be cleanned up with help of extenion methods

            services.AddSingleton<IIgnoredRequestProvider>(x =>
            {
                var activator = x.GetService<ITypeActivator>();

                var urlPolicy = activator.CreateInstances<IIgnoredRequestPolicy>(new []
                    {
                        typeof(UriIgnoredRequestPolicy).GetTypeInfo(),
                        typeof(ContentTypeIgnoredRequestPolicy).GetTypeInfo()
                    }); 
                 
                var provider = new FixedIgnoredRequestProvider(urlPolicy);

                return provider; 
            });
            */

            services
                .AddGlimpse()
                    .RunningAgentWeb(options =>
                    {
                        //options.IgnoredStatusCodes.Add(200);
                    });

            // TODO: Make this happen automatically if the file exists.
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("glimpse.json")
                .Build();
            services.Configure<ScriptOptions>(Configuration.GetSection("ScriptOptions"));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpseAgent();

            app.UseWelcomePage();
        }
    }
}
