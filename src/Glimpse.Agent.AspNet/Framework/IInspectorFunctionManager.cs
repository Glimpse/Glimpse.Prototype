using Microsoft.AspNet.Builder;

namespace Glimpse.Agent.AspNet
{
    public interface IInspectorFunctionManager
    {
        RequestDelegate BuildInspectorBranch(RequestDelegate next, IApplicationBuilder app);
    }
}