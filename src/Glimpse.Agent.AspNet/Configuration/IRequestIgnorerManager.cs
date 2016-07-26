using Microsoft.AspNetCore.Http;

namespace Glimpse.Agent.Configuration
{
    public interface IRequestIgnorerManager
    {
        bool ShouldIgnore();

        bool ShouldIgnore(HttpContext context);
    }
}
