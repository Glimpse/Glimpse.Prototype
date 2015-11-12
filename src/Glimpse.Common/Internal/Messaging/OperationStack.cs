using System;
using Glimpse.Common.Internal.Messaging;

namespace Glimpse.Internal
{
    public class OperationStack
    {
        private static readonly ContextData<OperationChain> _chainContext = new ContextData<OperationChain>();
        private static readonly ContextData<OperationOffset> _offsetContext = new ContextData<OperationOffset>();

        public void StartOffset()
        {
            StartOffset(DateTime.UtcNow);
        }
        public void StartOffset(DateTime startTime)
        {
            var operationOffset = new OperationOffset(startTime);
            _offsetContext.Value = operationOffset;
        }

        public void PushOperation(object item)
        {
            PushOperation(item, DateTime.UtcNow);
        }

        public void PushOperation(object item, DateTime dateTime)
        {
            var offset = _offsetContext.Value?.Elapsed;
            var operation = new Operation(item, dateTime, offset);

            var next = new OperationChain()
            {
                Operation = operation,
                Next = _chainContext.Value
            };

            _chainContext.Value = next;
        }

        public OperationTiming<T> PopOperation<T>()
        {
            var current = _chainContext.Value;
            _chainContext.Value = current?.Next;

            return new OperationTiming<T>(current?.Operation ?? default(Operation));
        }

        private class OperationChain
        {
            public Operation Operation { get; set; }

            public OperationChain Next { get; set; }
        }
    }
}
