using System;
using System.Linq;
using Glimpse.Agent.AspNet.Messages;
using Glimpse.Agent.Inspectors;
using Glimpse.Initialization;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.AspNet.Internal.Inspectors.AspNet
{
    // TODO: This is a toy implementation based on Inspector. We may want a better implementation.
    public class TabInspector : Inspector
    {
        private readonly IAgentBroker _broker;
        private readonly ITab[] _tabs;

        public TabInspector(IAgentBroker broker, IExtensionProvider<ITab> tabProvider)
        {
            _broker = broker;
            _tabs = tabProvider.Instances.ToArray();
        }

        public override void After(HttpContext context)
        {
            if (_tabs.Length == 0)
                return;

            foreach (var tab in _tabs)
            {
                object data = null;
                try
                {
                    data = tab.GetData(context);
                }
                catch (Exception exception)
                {
                    data = exception;
                }

                _broker.SendMessage(new TabMessage(tab.Name, data));
            }
        }
    }
}
