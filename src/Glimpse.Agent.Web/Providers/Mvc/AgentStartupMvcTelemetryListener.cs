using System.Diagnostics.Tracing;
using Glimpse.Agent.Web;
using Glimpse.Web;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.Agent.AspNet.Mvc
{
    public class AgentStartupMvcTelemetryListener : IAgentStartup
    {
        public AgentStartupMvcTelemetryListener(IRequestIgnorerManager requestIgnorerManager)
        {
            RequestIgnorerManager = requestIgnorerManager;
        }

        private IRequestIgnorerManager RequestIgnorerManager { get; }

        public void Run(IStartupOptions options)
        {
            var appServices = options.ApplicationServices;

            var telemetryListener = appServices.GetRequiredService<TelemetryListener>();
            telemetryListener.SubscribeWithAdapter(appServices.GetRequiredService<MvcTelemetryListener>(), IsEnabled);
        }

        private bool IsEnabled(string topic)
        {
            return !RequestIgnorerManager.ShouldIgnore();
        }
    }
}
