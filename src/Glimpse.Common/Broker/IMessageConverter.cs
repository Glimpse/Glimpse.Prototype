using System;

namespace Glimpse
{
    public interface IMessageConverter
    {
        IMessageEnvelope ConvertMessage(object payload);
    }
}