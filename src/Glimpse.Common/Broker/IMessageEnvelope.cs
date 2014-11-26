using System;

namespace Glimpse
{
    public interface IMessageEnvelope
    {
        string Type { get; }

        object Message { get; }
    }
}