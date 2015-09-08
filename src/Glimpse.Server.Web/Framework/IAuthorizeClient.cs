using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public interface IAuthorizeClient
    {
        bool AllowUser(HttpContext context);
    }
}