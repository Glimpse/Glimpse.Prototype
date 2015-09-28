using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public interface IRequestIgnorerManager
    {
        bool ShouldIgnore();

        bool ShouldIgnore(HttpContext context);
    }
}
