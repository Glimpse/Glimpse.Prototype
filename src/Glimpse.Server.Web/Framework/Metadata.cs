using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glimpse.Server.Web
{
    public class Metadata
    {
        public Metadata()
        {
            Resources = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
        }

        public IReadOnlyDictionary<string, string> Resources { get; set; }

        public string Hash { get; set; }
    }
}