using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web.Inspectors
{
    public class UserInspector : IInspector
    {
        public Task Before(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task After(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
