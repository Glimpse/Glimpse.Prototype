using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Glimpse.Server.Internal.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class JsonStringExtensions
    {
        public static string ToJsonArray(this IEnumerable<string> jsonStringCollection)
        {
            var sb = new StringBuilder("[");
            sb.Append(string.Join(",", jsonStringCollection));
            sb.Append("]");
            return sb.ToString();
        }
    }
}
