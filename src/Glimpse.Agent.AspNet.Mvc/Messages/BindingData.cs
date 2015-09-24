using System;

namespace Glimpse.Agent.AspNet.Mvc.Messages
{
    public class BindingData
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}