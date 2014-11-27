using System;

namespace Glimpse
{
    public class BaseMessage : IMessage
    {
        public BaseMessage()
        {
            Id = Guid.NewGuid();
            Time =  DateTime.Now;
        }

        public Guid Id { get; }

        public DateTime Time { get; }
    }
}