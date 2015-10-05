using System;

namespace Glimpse.Internal
{
    public interface IMessageConverter
    {
        IMessage ConvertMessage(object payload, MessageContext context);
    }
}