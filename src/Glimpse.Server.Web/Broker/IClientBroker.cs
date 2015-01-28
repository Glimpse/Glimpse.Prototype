using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public interface IClientBroker
    {
        Task PublishMessage(IMessageEnvelope message);
    }
}