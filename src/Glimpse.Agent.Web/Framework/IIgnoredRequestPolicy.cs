using System;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public interface IIgnoredRequestPolicy
    {
        bool ShouldIgnore(IHttpContext context);
    }
}