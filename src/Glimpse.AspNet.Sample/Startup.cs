using System;
using Microsoft.AspNet.Builder;
using Glimpse.Host.AspNet;

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
