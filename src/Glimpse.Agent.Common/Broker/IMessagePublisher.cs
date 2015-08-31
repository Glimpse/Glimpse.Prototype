using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IMessagePublisher
    {
        void PublishMessage(IMessage message);
    }
}