using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;

namespace Glimpse.Server.Connection.Inbound.Stream.Middleware
{
    public class WebSocketsComposer : IMiddlewareResourceComposer
    {
        public void Register(IApplicationBuilder appBuilder)
        {
            appBuilder.UseSignalR("/Data/Stream");
        }
    }
}
