using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public class InspectorsInspectorStartup : IInspectorStartup
    {
        private readonly IEnumerable<IInspector> _inspectors;

        public InspectorsInspectorStartup(IExtensionProvider<IInspector> inspectorProvider)
        {
            _inspectors = inspectorProvider.Instances;
        }

        public void Configure(IInspectorBuilder builder)
        {
            builder.Use(async (context, next) =>
            {
                foreach (var inspector in _inspectors)
                {
                    inspector.Before(context);
                }

                await next();

                foreach (var inspector in _inspectors)
                {
                    inspector.After(context);
                }
            });
        }
    }
}
