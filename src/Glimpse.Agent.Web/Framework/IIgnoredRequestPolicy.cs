using System;

namespace Glimpse.Agent.Web
{
    public interface IIgnoredRequestPolicy
    {
        bool ShouldIgnore(IContext context);
    }
}