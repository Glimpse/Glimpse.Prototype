using System;

namespace Glimpse.Server.Web
{
    public interface IServerBroker
    {
        /// <summary>
        /// Off the reciever thread and is not blocking
        /// </summary>
        ServerBrokerHook OffRecieverThread { get; }

        void SendMessage(IMessage message);
    }
}