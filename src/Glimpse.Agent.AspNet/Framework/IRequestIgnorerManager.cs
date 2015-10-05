using Microsoft.AspNet.Http;

namespace Glimpse.Agent.AspNet
{
    public interface IRequestIgnorerManager
    {
        bool ShouldIgnore();

        bool ShouldIgnore(HttpContext context);
    }
}
