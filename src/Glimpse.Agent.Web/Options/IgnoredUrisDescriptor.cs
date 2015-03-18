using System;
using System.Text.RegularExpressions;

namespace Glimpse.Agent.Web.Options
{
    public class IgnoredUrisDescriptor
    {
        public IgnoredUrisDescriptor(string format)
        {
            Expression = new Regex(format, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.None);
        }

        public IgnoredUrisDescriptor(Regex expression)
        {
            Expression = expression;
        }

        public Regex Expression { get; }
    }
}