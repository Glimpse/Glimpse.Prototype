using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Glimpse
{
    public interface IMessageTag
    {
        [JsonIgnore] // TODO: Not sure if we want this type leaked
        IEnumerable<string> Tags { get; set; }
    }
}