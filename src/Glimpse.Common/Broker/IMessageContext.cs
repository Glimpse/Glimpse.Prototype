using System;

namespace Glimpse.Broker
{
    public interface IMessageContext
    {
        Guid Id { get; set; }

        string Type { get; set; }
    }
}