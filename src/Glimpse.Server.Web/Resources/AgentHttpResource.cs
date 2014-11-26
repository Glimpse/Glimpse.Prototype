using Glimpse.Web;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Server.Resources
{
    public class AgentHttpResource : IRequestHandler
    {
        public bool WillHandle(IContext context)
        {
            return context.Request.Path.StartsWith("/Glimpse/Agent");
        }

        public async Task Handle(IContext context)
        {
            // TEST CODE ONLY!!!!
            var request = context.Request;
            var reader = new StreamReader(request.Body);
            var text = reader.ReadToEnd();

            var response = context.Response;

            response.SetHeader("Content-Type", "text/plain");

            var data = Encoding.UTF8.GetBytes(text);
            await response.WriteAsync(data);
            // TEST CODE ONLY!!!!
        }
    }
}