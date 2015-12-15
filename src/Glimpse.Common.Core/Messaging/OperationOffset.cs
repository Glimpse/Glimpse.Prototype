using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Messaging
{
    public class OperationOffset
    {
        private Stopwatch _timer;

        public OperationOffset()
            : this(DateTime.UtcNow)
        {
        }

        public OperationOffset(DateTime startTime)
        {
            StartTime = startTime;
            _timer = new Stopwatch();
            _timer.Start();
        }

        public TimeSpan Elapsed => _timer.Elapsed;

        public DateTime StartTime { get; set; }
    }
}
