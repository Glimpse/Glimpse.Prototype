using System;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public interface IClientBroker
    {
        void PublishMessage(IMessage message);
    }
}