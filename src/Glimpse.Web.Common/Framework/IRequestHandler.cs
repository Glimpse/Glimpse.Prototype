using System;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public interface IRequestHandler
    {
        bool WillHandle(IContext context);

        Task Handle(IContext context);
    }
}