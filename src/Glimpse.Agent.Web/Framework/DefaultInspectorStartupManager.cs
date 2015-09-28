using System.Collections.Generic;
using Microsoft.AspNet.Builder;

namespace Glimpse.Agent.Web
{
    public class DefaultInspectorStartupManager : IInspectorStartupManager
    {
        public DefaultInspectorStartupManager(IExtensionProvider<IInspectorStartup> inspectorStartupProvider)
        {
            InspectorStartups = inspectorStartupProvider.Instances;
        }

        private IEnumerable<IInspectorStartup> InspectorStartups { get; }

        public RequestDelegate BuildInspectorBranch(RequestDelegate next, IApplicationBuilder app)
        {
            return BuildInspectorBranch(next, app, InspectorStartups);
        }

        public RequestDelegate BuildInspectorBranch(RequestDelegate next, IApplicationBuilder app, IEnumerable<IInspectorStartup> inspectorStartups)
        {
            // create new pipeline
            var branchBuilder = app.New();
            foreach (var middlewareProfiler in inspectorStartups)
            {
                middlewareProfiler.Configure(new InspectorBuilder(branchBuilder));
            }
            branchBuilder.Use(subNext => { return async ctx => await next(ctx); });

            return branchBuilder.Build();
        }
    }
}