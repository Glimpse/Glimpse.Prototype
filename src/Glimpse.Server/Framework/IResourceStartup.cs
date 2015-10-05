namespace Glimpse.Server.Web
{
    public interface IResourceStartup
    {
        void Configure(IResourceBuilder resourceBuilder);

        ResourceType Type { get; }
    }
}