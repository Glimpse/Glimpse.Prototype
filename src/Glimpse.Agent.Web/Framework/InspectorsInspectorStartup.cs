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
            builder.Use(next => async context =>
            {
                foreach (var inspector in _inspectors)
                {
                    await inspector.Before(context);
                }

                await next(context);

                foreach (var inspector in _inspectors)
                {
                    await inspector.After(context);
                }
            });
        }
    }
}
