using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Builder;
using Glimpse.Host.Web.AspNet;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Agent.AspNet.Sample
{
    public class Startup
    {
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

            services.AddGlimpse()
                .RunningAgentWeb()
                        .ConfigureAgentWeb(options =>
                        {
                            //options.IgnoredStatusCodes.Add(200);
                        }) 
                    .WithRemoteStreamAgent(); 
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpse();

            app.UseWelcomePage();
        }
    }
}
