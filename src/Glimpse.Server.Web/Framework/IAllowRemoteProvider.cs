using System;

namespace Glimpse.Server
{
    public interface IAllowRemoteProvider
    {
        bool AllowRemote { get; }
    }
}