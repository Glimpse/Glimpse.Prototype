using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IMessagePublisher
    {
        Task PublishMessage(IMessage message);
    }
}