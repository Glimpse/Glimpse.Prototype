using System.Collections.Generic;

namespace Glimpse.Common.Messaging
{
    public interface IMessageTypeProvider
    {
        IEnumerable<string> Types { get; }
    }
}