using System;

namespace Glimpse.Common.Broker
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PromoteToUrlAttribute : PromoteToAttribute
    {
        public PromoteToUrlAttribute() : base("request-url")
        {
        }
    }
}
