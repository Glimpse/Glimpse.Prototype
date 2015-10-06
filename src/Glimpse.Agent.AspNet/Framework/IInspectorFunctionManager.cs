using Microsoft.AspNet.Builder;

namespace Glimpse.Agent
{
    public interface IInspectorFunctionManager
    {
        RequestDelegate BuildInspectorBranch(RequestDelegate next, IApplicationBuilder app);
    }
}