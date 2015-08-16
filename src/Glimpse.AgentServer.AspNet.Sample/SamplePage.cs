using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using System.Text;
using Microsoft.AspNet.Http;

namespace Glimpse.AspNet.Sample
{
    public class SamplePage
    {
        private readonly ContextData<MessageContext> _context;

        public SamplePage()
        {
            _context = new ContextData<MessageContext>();
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;

            response.Headers.Set("Content-Type", "text/html");
             
            await response.WriteAsync($"<html><body><h1>Agent Test</h1><script src='/Glimpse/Browser/Agent' id='glimpse' data-glimpse-id='{_context?.Value?.Id}'></script></body></html>");
        }
    }
}