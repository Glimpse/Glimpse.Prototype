#if SystemWeb
using System.Web;
using SystemWebAdapter;
using Glimpse.Server.Resources;

namespace Glimpse
{
    public class GlimpseServerHandler : IHttpHandler
    {
        private readonly IResourceRuntimeManager _resourceRuntimeManager;

        public GlimpseServerHandler(IResourceRuntimeManager resourceRuntimeManager)
        {
            _resourceRuntimeManager = resourceRuntimeManager;

            // TODO: Need to check if this only runs once or multiple times
            _resourceRuntimeManager.Register();
        }

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext httpContext)
        {
            var context = GetHttpContext(httpContext);

            // need to wait as ProcessRequest is sync
            _resourceRuntimeManager.ProcessRequest(context).Wait();
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