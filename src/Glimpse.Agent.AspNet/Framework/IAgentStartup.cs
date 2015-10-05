using Glimpse.Web;

namespace Glimpse.Agent.AspNet
{
    public interface IAgentStartup
    {
        void Run(IStartupOptions options);
    }
}
