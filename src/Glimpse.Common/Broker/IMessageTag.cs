using System;
using System.Collections.Generic;

namespace Glimpse
{
    public interface IMessageTag
    {
        IEnumerable<string> Tags { get; }
    }
}