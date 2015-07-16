using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IChannelSender
    {
        Task PublishMessage(IMessageEnvelope message);
    }
}