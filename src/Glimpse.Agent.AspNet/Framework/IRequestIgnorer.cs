using Microsoft.AspNet.Http;

namespace Glimpse.Agent
{
    public interface IRequestIgnorer
    {
        bool ShouldIgnore(HttpContext context);
    }
}