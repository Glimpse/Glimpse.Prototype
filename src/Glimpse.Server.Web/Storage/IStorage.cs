using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public interface IStorage
    {
        void Persist(IMessage message);

        Task<IEnumerable<IMessage>> RetrieveBy(Guid id);
    }
}