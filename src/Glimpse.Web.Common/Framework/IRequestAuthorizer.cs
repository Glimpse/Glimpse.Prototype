using Microsoft.AspNet.Http;

namespace Glimpse.Web
{
    public interface IRequestAuthorizer
    {
        bool AllowUser(HttpContext context);
    }
}