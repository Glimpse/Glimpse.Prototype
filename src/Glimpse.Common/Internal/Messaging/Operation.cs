using System;
using System.Diagnostics;

namespace Glimpse.Internal
{
    public struct Operation
    {
        public Operation(object item)
            : this(item, DateTime.UtcNow)
        {
        }

        public Operation(object item, DateTime start)
        {
            Item = item;
            Start = start;
            Timer = Stopwatch.StartNew();
        }

        public object Item { get; set; }

        public DateTime Start { get; }

        public Stopwatch Timer { get; }
    }
}
