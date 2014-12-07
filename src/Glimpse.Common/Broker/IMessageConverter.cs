using System;

namespace Glimpse
{
    public interface IMessageConverter
    {
        IMessageEnvelope ConvertMessage(IMessage message);
    }
}