using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public interface IRequestHandler
    {
        bool WillHandle(HttpContext context);

        Task Handle(HttpContext context);
    }
}