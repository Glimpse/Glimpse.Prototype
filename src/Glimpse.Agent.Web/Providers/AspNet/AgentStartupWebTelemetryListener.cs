using System.Diagnostics.Tracing;
using Glimpse.Web;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Agent.Web
{
    public class AgentStartupWebTelemetryListener : IAgentStartup
    {
        public AgentStartupWebTelemetryListener(IRequestIgnorerManager requestIgnorerManager)
        {
            RequestIgnorerManager = requestIgnorerManager;
        }

        private IRequestIgnorerManager RequestIgnorerManager { get; }

        public void Run(IStartupOptions options)
        {
            var appServices = options.ApplicationServices;

            var telemetryListener = appServices.GetRequiredService<TelemetryListener>();
            telemetryListener.SubscribeWithAdapter(appServices.GetRequiredService<AspNetTelemetryListener>(), IsEnabled);
        }

        private bool IsEnabled(string topic)
        {
            return !RequestIgnorerManager.ShouldIgnore();
        }
    }
}
