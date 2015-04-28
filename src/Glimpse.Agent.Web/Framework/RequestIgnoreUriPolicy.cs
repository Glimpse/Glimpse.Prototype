using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Glimpse.Agent.Web;
using Glimpse.Web;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Agent.Web.Framework
{
    public class RequestIgnoreUriPolicy : IRequestIgnorePolicy
    {
        private readonly IReadOnlyCollection<Regex> _ignoredUris;

        public RequestIgnoreUriPolicy(IIgnoredUriProvider ignoredUriProvider)
        {
            _ignoredUris = ignoredUriProvider.IgnoredUris;
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