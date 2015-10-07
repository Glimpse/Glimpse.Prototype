using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Server.Resources;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Internal.Resources
{
    //TODO: "Merge" with ClientResource
    public class SpaClientResource : IResource
    {
        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "text/html";

            await response.WriteAsync(@"
<html>
<head><meta http-equiv='refresh' content='3; url=/glimpse/client/index.html' ></head>
<body><h1>We must reconcile this with <a href='/glimpse/client/index.html'>ClientResource</a>.</h1><p>Auto redirect in 3 seconds....</p></body>
</html>");
        }

        public string Name => "SpaClientScript";
        public IEnumerable<ResourceParameter> Parameters => new []{ +ResourceParameter.Hash, ResourceParameter.RequestId};
        public ResourceType Type => ResourceType.Client;
    }
}
