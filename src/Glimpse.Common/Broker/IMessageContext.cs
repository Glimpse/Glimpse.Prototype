using System;

namespace Glimpse
{
    // TODO: currently an interface as I'm not sure if different
    //       agent types may want to add extra properties 
    public interface IMessageContext
    {
        Guid Id { get; set; }

        string Type { get; set; }
    }
}