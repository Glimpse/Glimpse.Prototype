namespace Glimpse.Server.Web
{
    public interface IClientBroker
    {
        void PublishMessage(IMessage message);
    }
}