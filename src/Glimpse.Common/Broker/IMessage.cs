using System;

namespace Glimpse
{
    public interface IMessage
    {
        Guid Id { get; }

        DateTime Time { get; }
    }
}