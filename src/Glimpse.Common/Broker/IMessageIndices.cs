using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Glimpse
{
    public interface IMessageIndices
    {
        [JsonIgnore] // TODO: Not sure if we want this type leaked
        IReadOnlyDictionary<string, object> Indices { get; set; }
    }
}