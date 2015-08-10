using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public interface IRequestIgnorer
    {
        bool ShouldIgnore(HttpContext context);
    }
}