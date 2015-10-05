using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public interface IAllowAgentAccess
    {
        bool AllowAgent(HttpContext context);
    }
}
