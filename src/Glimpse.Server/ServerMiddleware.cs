using Glimpse.Common.Initialization;
using Microsoft.AspNetCore.Builder;

namespace Glimpse.Server
{
    public class ServerMiddleware : IRegisterMiddleware
    {
        public void RegisterMiddleware(IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<GlimpseServerMiddleware>(appBuilder);
        }
    }
}