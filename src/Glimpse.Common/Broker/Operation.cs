using System;

namespace Glimpse
{
    public struct Operation
    {
        public Operation(object item)
        {
            Item = item;
            Start = DateTime.UtcNow;
        }

        public Operation(object item, DateTime start)
        {
            Item = item;
            Start = start;
        }

        public object Item { get; set; }

        public DateTime Start { get; set; }
    }
}
