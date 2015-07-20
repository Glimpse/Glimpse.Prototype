using System;

namespace Glimpse
{
    public struct Timing
    {
        public Timing(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public TimeSpan Elapsed => End - Start;
    }
}
