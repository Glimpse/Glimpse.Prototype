using System;
using Glimpse.Web;

namespace Glimpse.Agent.Web
{
    public interface IRequestIgnorer
    {
        bool ShouldIgnore(IHttpContext context);
    }
}