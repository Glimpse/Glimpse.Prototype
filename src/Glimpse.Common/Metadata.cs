using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glimpse
{
    public class Metadata : IMetadata
    {
        public Metadata()
        {
            Resources = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
        }

        public ReadOnlyDictionary<string, string> Resources { get; set; }

        public string Hash { get; set; }
    }
}