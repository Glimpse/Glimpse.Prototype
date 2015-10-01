using System.Text.RegularExpressions;

namespace Glimpse.Common
{
    public static class StringExtensions
    {
        private readonly static Regex _regex = new Regex("([A-Z0-9])", RegexOptions.Compiled);

        public static string KebabCase(this string input)
        {
            return _regex.Replace(input, Replacement);
        }

        private static string Replacement(Match m)
        {
            var result = m.Value.ToLower();
            return m.Index == 0 ? result : "-" + result;
        }
    }
}
