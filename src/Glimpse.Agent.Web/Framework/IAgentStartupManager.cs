using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public interface IAgentStartupManager
    {
        void Run(IStartupOptions options);
    }
}
