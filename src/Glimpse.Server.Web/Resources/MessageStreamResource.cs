namespace Glimpse.Server.Web
{
    public class MessageStreamResource : IResourceStartup
    {
        private readonly IServerBroker _serverBroker;

        public MessageStreamResource(IServerBroker serverBroker)
        {
            _serverBroker = serverBroker;
        }

        public void Configure(IResourceBuilder resourceBuilder)
        {
            // TODO: Need to get data to the client
            throw new System.NotImplementedException();
        }
    }
}
