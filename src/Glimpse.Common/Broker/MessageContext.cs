using System;

namespace Glimpse.Broker
{
    public class MessageContext : IMessageContext
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
    }
}