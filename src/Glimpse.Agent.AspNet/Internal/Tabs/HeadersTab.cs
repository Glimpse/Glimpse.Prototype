using Glimpse.Agent.Inspectors;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.AspNet.Internal.Tabs
{
    // TODO: Delete me. This tab isn't intended to stick around, it's just a sample of the ITab interface.
    public class HeadersTab : ITab
    {
        public string Name => "Headers";

        public object GetData(HttpContext context)
        {
            return new
            {
                Request = context.Request.Headers,
                Response = context.Response.Headers
            };
        }
    }
}
