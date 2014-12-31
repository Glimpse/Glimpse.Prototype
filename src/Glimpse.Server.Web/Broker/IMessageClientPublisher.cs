using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public interface IMessageClientPublisher
    {
        Task PublishMessage(IMessageEnvelope message);
    }
}