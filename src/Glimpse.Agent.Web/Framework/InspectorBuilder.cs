using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Builder;

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
            return new InspectorBuilder(AppBuilder.Use(middleware));
        }
    }
}