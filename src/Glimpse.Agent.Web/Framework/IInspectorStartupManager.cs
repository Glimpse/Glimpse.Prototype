using Microsoft.AspNet.Builder;

namespace Glimpse.Agent.Web
{
    public interface IInspectorStartupManager
    {
        RequestDelegate BuildInspectorBranch(RequestDelegate next, IApplicationBuilder app);
    }
}