using System.Collections.Generic;

namespace Glimpse.Server.Web
{
    public interface IRequestAuthorizerProvider
    {
        IEnumerable<IRequestAuthorizer> Authorizers { get; }
    }
}