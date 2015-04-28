using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web
{
    public class DefaultIgnoredStatusCodeProvider : IIgnoredStatusCodeProvider
    {
        public DefaultIgnoredStatusCodeProvider(IOptions<GlimpseAgentWebOptions> optionsAccessor)
        {
            var statusCodes = optionsAccessor.Options.IgnoredStatusCodes;
            StatusCodes = statusCodes.ToList();
        }

        public IReadOnlyList<int> StatusCodes { get; } 
    }
}