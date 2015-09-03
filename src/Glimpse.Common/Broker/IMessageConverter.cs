using System;

namespace Glimpse
{
    public interface IMessageConverter
    {
        IMessage ConvertMessage(object payload, MessageContext context);
    }
}