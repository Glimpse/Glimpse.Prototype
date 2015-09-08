using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web
{
    public interface IAuthorizeAgent
    {
        bool AllowAgent(HttpContext context);
    }
}
