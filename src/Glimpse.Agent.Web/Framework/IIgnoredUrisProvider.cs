using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Glimpse.Agent.Web.Options
{
    public interface IIgnoredUrisProvider
    {
        IReadOnlyList<Regex> IgnoredUris { get; }
    }
}