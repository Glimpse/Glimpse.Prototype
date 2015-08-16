using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public interface IRequestAuthorizer
    {
        bool AllowUser(HttpContext context);
    }
}