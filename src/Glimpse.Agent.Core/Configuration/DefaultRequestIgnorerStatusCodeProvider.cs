using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Agent.Configuration
{
    public class DefaultRequestIgnorerStatusCodeProvider : IRequestIgnorerStatusCodeProvider
    {
        public DefaultRequestIgnorerStatusCodeProvider(IOptions<GlimpseAgentOptions> optionsAccessor)
        {
            var statusCodes = optionsAccessor.Value.IgnoredStatusCodes;
            StatusCodes = statusCodes.ToList();
        }

        public IReadOnlyList<int> StatusCodes { get; } 
    }
}