using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public class InspectorsInspectorStartup : IInspectorStartup
    {
        private readonly IEnumerable<IInspector> _inspectors;

        public InspectorsInspectorStartup(IInspectorProvider inspectorProvider)
        {
            _inspectors = inspectorProvider.Inspectors;
        }

        public void Configure(IInspectorBuilder builder)
        {
            builder.Use(async (context, next) =>
            {
                foreach (var inspector in _inspectors)
                {
                    await inspector.Before(context);
                }

                await next();

                foreach (var inspector in _inspectors)
                {
                    await inspector.After(context);
                }
            });
        }
    }
}
