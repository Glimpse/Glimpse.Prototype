using Microsoft.AspNet.Builder;

namespace Glimpse.Agent.Web
{
    public interface IInspectorFunctionManager
    {
        RequestDelegate BuildInspectorBranch(RequestDelegate next, IApplicationBuilder app);
    }
}