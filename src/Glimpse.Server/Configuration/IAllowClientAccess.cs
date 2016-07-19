using Microsoft.AspNetCore.Http;

namespace Glimpse.Server.Configuration
{
    public interface IAllowClientAccess
    {
        bool AllowUser(HttpContext context);
    }
}