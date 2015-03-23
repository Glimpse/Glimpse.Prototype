using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Glimpse.Web;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web.Framework
{
    public class UriIgnoredRequestPolicy : IIgnoredRequestPolicy
    {
        private readonly IList<Regex> _ignoredUris;

        public UriIgnoredRequestPolicy(IOptions<GlimpseAgentWebOptions> optionsAccessor)
        {
            _ignoredUris = optionsAccessor.Options.IgnoredUris;
        }

        public bool ShouldIgnore(IHttpContext context)
        {

            try
            {
                if (_ignoredUris.Count > 0)
                {  
                    var uri = context.Request.UriAbsolute();

                    if (_ignoredUris.Any(regex => regex.IsMatch(uri)))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception exception)
            {
                // TODO: Log this occurance
                // return true;
                throw;
            }
        }
    }
}