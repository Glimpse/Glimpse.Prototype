using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web.Framework
{
    public class Inspector : IInspector
    {
        public virtual void Before(HttpContext context)
        {
        }

        public virtual void After(HttpContext context)
        {
        }
    }
}
