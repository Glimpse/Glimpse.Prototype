using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace Glimpse.Agent.Connection.Stream.Connection
{
    internal class SignalrStreamHubProxyFactory : IStreamHubProxyFactory, IDisposable
    {
        private readonly Lazy<Task> _lazyInnerStart;
        private readonly IDictionary<string, Action<IStreamHubProxy>> _registrations; 
        private HubConnection _connection;

        public SignalrStreamHubProxyFactory()
        {
            _lazyInnerStart = new Lazy<Task>(() => InnerStart());
            _registrations = new Dictionary<string, Action<IStreamHubProxy>>();
        }

        public void Register(string hubName, Action<IStreamHubProxy> proxyCallback)
        {
            _registrations.Add(hubName, proxyCallback);
        }

        public async Task Start()
        {
            await _lazyInnerStart.Value;
        }
 
        public void Dispose()
        {
            _connection?.Dispose();
        }

        protected async Task InnerStart()
        {
            try
            {
                // TODO: This needs to get out of config
                var hubConnection = new HubConnection("http://localhost:5250/Glimpse/Data/Stream");

                SetupHubProxies(hubConnection);

                await hubConnection.Start();

                _connection = hubConnection;
            }
            catch (Exception e)
            {
                // TODO: Need to do something with the exeception
            }
        }

        protected void SetupHubProxies(HubConnection connection)
        {
            foreach (var registration in _registrations)
            {
                var hubProxy = connection.CreateHubProxy(registration.Key);
                var newHubProxy = new SignalrStreamHubProxy(hubProxy);

                registration.Value(newHubProxy);
            } 
        }
    } 
}