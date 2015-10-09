using System.Collections.Generic;
using Tavis.UriTemplates;

namespace Glimpse.Server.Internal.Extensions
{
    public static class UriTemplateExtensions
    {
        public static string ResolveWith(this UriTemplate uriTemplate, IDictionary<string, object> parameters)
        {
            uriTemplate.AddParameters(parameters);
            return uriTemplate.Resolve();
        }
    }
}
