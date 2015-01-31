using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class InProcessMessageStore : IStoragePublisher
    {
        private readonly IList<IMessageEnvelope> _store;

        public InProcessMessageStore()
        {
            _store = new List<IMessageEnvelope>();
        }

        public async Task StoreMessage(IMessageEnvelope message)
        {
            await Task.Run(() => _store.Add(message));
        }

        public IEnumerable<IMessageEnvelope> AllMessages
        {
            get { return _store; }
        }
    }
}