using Microsoft.AspNet.Http;

namespace Glimpse.Agent.AspNet
{
    public interface IRequestIgnorer
    {
        bool ShouldIgnore(HttpContext context);
    }
}