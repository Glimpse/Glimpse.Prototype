using System;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public interface IRequestIgnorePolicy
    {
        bool ShouldIgnore(IHttpContext context);
    }
}