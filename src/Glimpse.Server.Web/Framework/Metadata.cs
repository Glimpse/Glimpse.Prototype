using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glimpse.Server.Web
{
    public class Metadata
    {
        public Metadata(IDictionary<string, string> resources)
        {
            Resources = new ReadOnlyDictionary<string, string>(resources);
        }

        public IReadOnlyDictionary<string, string> Resources { get; set; }

        public string Hash { get; set; }
    }
}