using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public interface IAllowClientAccess
    {
        bool AllowUser(HttpContext context);
    }
}