using Microsoft.AspNet.Http;

namespace Glimpse.Agent
{
    public interface IRequestIgnorerManager
    {
        bool ShouldIgnore();

        bool ShouldIgnore(HttpContext context);
    }
}
