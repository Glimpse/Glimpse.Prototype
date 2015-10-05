using Glimpse.Web;

namespace Glimpse.Agent.AspNet
{
    public interface IAgentStartupManager
    {
        void Run(IStartupOptions options);
    }
}
