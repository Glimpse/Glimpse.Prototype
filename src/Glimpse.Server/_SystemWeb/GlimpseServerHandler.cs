#if SystemWeb
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SystemWebAdapter;

namespace Glimpse
{
    public class GlimpseServerHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
        private static Microsoft.AspNet.Http.HttpContext GetHttpContext(object sender)
        {
            var httpApplication = (HttpApplication)sender;

            // convert SystemWeb HttpContext to DNX HttpContext 
            return httpApplication.Context.CreateContext();
        }
    }
}
#endif