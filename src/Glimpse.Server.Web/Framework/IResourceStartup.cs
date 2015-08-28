using Glimpse.Web;

namespace Glimpse.Server.Web
{
    public interface IResourceStartup
    {
        void Configure(IResourceBuilder resourceBuilder);
    }
}