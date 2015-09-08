using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public interface IClientAuthorizer
    {
        bool AllowUser(HttpContext context);
    }
}