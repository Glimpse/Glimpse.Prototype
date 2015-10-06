using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Glimpse.Agent.Configuration
{
    public static class RegexExtensions
    {
        public static void AddCompiled(this ICollection<Regex> collection, string expression)
        {
            var regex = new Regex(expression, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.None);
            collection.Add(regex);
        }
    }
}