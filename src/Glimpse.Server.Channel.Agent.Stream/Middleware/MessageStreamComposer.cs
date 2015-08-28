using Glimpse.Server.Web;
using Microsoft.AspNet.Builder;

namespace Glimpse.Server.Connection.Inbound.Stream.Middleware
{
    public class MessageStreamComposer : IResourceStartup
    {
        public void Configure(IResourceBuilder resourceBuilder)
        {
            resourceBuilder.AppBuilder.UseSignalR("/MessageStream");
        }
    }
}
