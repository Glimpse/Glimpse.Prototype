using System;

namespace Glimpse
{
    public struct OperationTiming<T>
    {
        public OperationTiming(Operation operation)
        {
            Item = (T)operation.Item;
            Start = operation.Start;

            End = DateTime.UtcNow;
        }

        public OperationTiming(T item, DateTime start, DateTime end)
        {
            Item = item;
            Start = start;
            End = end;
        }

        public T Item { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public TimeSpan Elapsed => End - Start;

        public Timing Timing => new Timing(Start, End);
    }
}
