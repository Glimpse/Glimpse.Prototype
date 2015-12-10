using System;
using Microsoft.AspNet.Builder;

namespace Glimpse.Initialization
{
    public class StartupOptions : IStartupOptions
    {
        public StartupOptions(IApplicationBuilder builder)
        {
            Builder = builder;
        }

        private IApplicationBuilder Builder { get; }

        public IServiceProvider ApplicationServices => Builder.ApplicationServices;
    }
}