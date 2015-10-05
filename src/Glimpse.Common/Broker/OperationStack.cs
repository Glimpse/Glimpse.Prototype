
using System.Collections.Generic;
using Glimpse.Internal;

namespace Glimpse
{
    public class OperationStack
    {
        private ContextData<OperationChain> _context = new ContextData<OperationChain>();

        public void PushOperation(Operation operation)
        {
            var next = new OperationChain()
            {
                Operation = operation,
                Next = _context.Value
            };

            _context.Value = next;
        }

        public void PushOperation(object item)
        {
            PushOperation(new Operation(item));
        }

        public OperationTiming<T> PopOperation<T>()
        {
            var current = _context.Value;
            _context.Value = current?.Next;

            return new OperationTiming<T>(current?.Operation ?? default(Operation));
        }

        private class OperationChain
        {
            public Operation Operation { get; set; }

            public OperationChain Next { get; set; }
        }
    }
}
