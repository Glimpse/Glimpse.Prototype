using System;

namespace Glimpse.Broker
{
    public interface IMessage
    {
        Guid Id { get; set; }

        DateTime Time { get; set; }
    }
}