using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public interface IChannelSender
    {
        void PublishMessage(IMessage message);
    }
}