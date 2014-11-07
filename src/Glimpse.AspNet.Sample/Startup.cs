using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.AspNet.Sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpse();
        }
    }
}
