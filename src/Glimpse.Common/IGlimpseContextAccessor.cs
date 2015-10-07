using System;

namespace Glimpse.Common
{
    public interface IGlimpseContextAccessor
    {
        Guid RequestId { get; }
    }
}
