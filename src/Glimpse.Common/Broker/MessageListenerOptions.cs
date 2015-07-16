using System;

namespace Glimpse
{
    public class MessageListenerOptions
    {
        public MessageListenerOptions(IMessage message)
        {
            Message = message;
        }

        public IMessage Message { get; set; }

        public bool IsCancelled { get; set; }
    }
}