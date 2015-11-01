namespace Glimpse.Agent.Internal.Inspectors.Mvc.Proxies
{
    public interface IViewComponentDescriptor
    {
        string Id { get; set; }
        string FullName { get; set; }
        string ShortName { get; set; }
    }
}