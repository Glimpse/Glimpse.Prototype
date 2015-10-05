using System.Diagnostics.Tracing;
using Glimpse.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.Agent.AspNet
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
            telemetryListener.SubscribeWithAdapter(appServices.GetRequiredService<WebTelemetryListener>(), IsEnabled);
        }

        private bool IsEnabled(string topic)
        {
            return !RequestIgnorerManager.ShouldIgnore();
        }
    }
}
