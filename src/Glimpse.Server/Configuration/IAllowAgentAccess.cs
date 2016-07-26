using Microsoft.AspNetCore.Http;

namespace Glimpse.Server.Configuration
{
    public interface IAllowAgentAccess
    {
        bool AllowAgent(HttpContext context);
    }
}
