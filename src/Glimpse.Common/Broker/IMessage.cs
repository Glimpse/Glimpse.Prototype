using System;

namespace Glimpse
{
    public interface IMessage
    {
        Guid Id { get; set; }

        DateTime Time { get; set; }
    }
}