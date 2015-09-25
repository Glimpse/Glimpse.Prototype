using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public interface IAgentStartup
    {
        void Run(IStartupOptions options);
    }
}
