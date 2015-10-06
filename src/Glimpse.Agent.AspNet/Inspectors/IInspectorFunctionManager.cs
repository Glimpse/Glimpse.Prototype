using Microsoft.AspNet.Builder;

namespace Glimpse.Agent.Inspectors
{
    public interface IInspectorFunctionManager
    {
        RequestDelegate BuildInspectorBranch(RequestDelegate next, IApplicationBuilder app);
    }
}