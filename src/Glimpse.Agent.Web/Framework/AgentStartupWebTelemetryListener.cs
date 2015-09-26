using System.Diagnostics.Tracing;
using Glimpse.Web;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Agent.Web
{
    public class AgentStartupWebTelemetryListener : IAgentStartup
    {
        public void Run(IStartupOptions options)
        {
            var appServices = options.ApplicationServices;

            var telemetryListener = appServices.GetRequiredService<TelemetryListener>();
            telemetryListener.SubscribeWithAdapter(appServices.GetRequiredService<WebTelemetryListener>());
        }
    }
}
