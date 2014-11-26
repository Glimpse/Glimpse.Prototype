using System;

namespace Glimpse.Broker
{

    public class MessageEnvelope
    {
        string Type { get; set; }

        object Message { get; set; }
    }
}