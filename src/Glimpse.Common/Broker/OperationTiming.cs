using System;
using System.Diagnostics;
using System.Threading;

namespace Glimpse
{
    public struct OperationTiming<T>
    {
        private readonly Stopwatch _timer;

        public OperationTiming(Operation operation)
            : this((T)operation.Item, operation.Start, DateTime.UtcNow, operation.Timer)
        {
        }

        public OperationTiming(T item, DateTime start, DateTime end, Stopwatch timer)
        {
            timer.Stop();

            Item = item;
            Start = start;
            End = end;
            _timer = timer;
        }

        public T Item { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public TimeSpan Elapsed => _timer.Elapsed;

        public Timing Timing => new Timing(Start, End, _timer.Elapsed);
    }
}
