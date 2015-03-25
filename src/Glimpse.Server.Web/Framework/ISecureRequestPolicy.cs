using System;
using Glimpse.Web;

namespace Glimpse.Server
{
    public interface ISecureRequestPolicy
    {
        bool AllowUser(IHttpContext context);
    }
}