using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public interface IStorage
    {
        Task Persist(IMessageEnvelope message);

        Task<IEnumerable<IMessageEnvelope>> RetrieveBy(Guid id);
    }
}