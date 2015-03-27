using System;

namespace Glimpse.Web
{
    public interface IRequestAuthorizer
    {
        bool AllowUser(IHttpContext context);
    }
}