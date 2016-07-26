using Microsoft.AspNetCore.Http;

namespace Glimpse.Agent.Configuration
{
    public interface IRequestIgnorer
    {
        bool ShouldIgnore(HttpContext context);
    }
}