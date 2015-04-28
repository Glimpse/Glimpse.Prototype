using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Glimpse.Agent.Web
{
    public interface IRequestIgnorerUriProvider
    {
        IReadOnlyList<Regex> IgnoredUris { get; }
    }
}