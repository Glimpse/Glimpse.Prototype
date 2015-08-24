using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Server.Web
{
    public interface IStorage
    {
        void Persist(IMessage message);

        Task<IEnumerable<IMessage>> RetrieveByType(params string[] types);
    }
}