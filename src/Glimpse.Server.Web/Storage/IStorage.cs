using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public interface IStorage
    {
        Task Persist(IMessageEnvelope message);
    }
}