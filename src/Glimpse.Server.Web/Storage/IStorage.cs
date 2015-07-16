using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public interface IStorage
    {
        Task Persist(IMessage message);

        Task<IEnumerable<IMessage>> RetrieveBy(Guid id);
    }
}