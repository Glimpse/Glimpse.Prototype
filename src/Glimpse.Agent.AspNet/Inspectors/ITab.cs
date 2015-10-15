using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Inspectors
{
    // TODO: Is this what we want to name this type?
    public interface ITab
    {
        string Name { get; }

        // TODO: Do we need to let user's specify before/after like Glimpse 1.0 did?

        // TODO: Is HttpContext the right type to pass in here?
        object GetData(HttpContext context);
    }
}