using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Glimpse.Server.Resources
{
    public interface IResponse
    {
        Task Respond(HttpContext context);
    }
}