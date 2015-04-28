using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Glimpse.Agent.Web.Options
{
    public interface IIgnoredUriProvider
    {
        IReadOnlyList<Regex> IgnoredUris { get; }
    }
}