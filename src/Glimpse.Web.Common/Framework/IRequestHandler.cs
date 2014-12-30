using System;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public interface IRequestHandler
    {
        bool WillHandle(IHttpContext context);

        Task Handle(IHttpContext context);
    }
}