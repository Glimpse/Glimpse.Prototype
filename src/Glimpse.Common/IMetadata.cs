using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glimpse
{
    public interface IMetadata
    {
        ReadOnlyDictionary<string, string> Resources { get; set; }

        string Hash { get; set; }
    }
}