using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public interface IStoragePublisher
    {
        Task StoreMessage(IMessageEnvelope message);
    }
}