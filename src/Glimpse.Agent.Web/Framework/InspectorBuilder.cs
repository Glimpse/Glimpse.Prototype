using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public class InspectorBuilder : IInspectorBuilder
    {
        public InspectorBuilder(IApplicationBuilder app)
        {
            AppBuilder = app;
        }

        public IApplicationBuilder AppBuilder { get; }
        
        public IInspectorBuilder Use(Func<HttpContext, Func<Task>, Task> middleware)
        {
            var newAppBuilder = AppBuilder.Use(middleware);
            return (newAppBuilder != AppBuilder) ? new InspectorBuilder(newAppBuilder) : this;
        }
    }
}