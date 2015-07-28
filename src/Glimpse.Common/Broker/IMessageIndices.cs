using System;
using System.Collections.Generic;

namespace Glimpse
{
    public interface IMessageIndices
    {
        IReadOnlyDictionary<string, object> Indices { get; set; }
    }
}