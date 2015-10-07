namespace Glimpse.Agent.Internal.Messaging
{
    public interface IMessageConverter
    {
        IMessage ConvertMessage(object payload, MessageContext context);
    }
}