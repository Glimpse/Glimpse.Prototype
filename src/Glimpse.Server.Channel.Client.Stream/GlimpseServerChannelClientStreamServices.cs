using Microsoft.Framework.DependencyInjection;
using Glimpse.Server.Web;

namespace Glimpse
{
    public class GlimpseServerChannelClientStreamServices
    {
        public static IServiceCollection GetDefaultServices()
        {
            var services = new ServiceCollection();
            
            // TODO: Config isn't currently being handled - https://github.com/aspnet/SignalR-Server/issues/51
            //services.AddSignalR(options =>
            //{
            //    options.Hubs.EnableDetailedErrors = true;
            //});
            
            //
            // Broker
            //
            services.AddSingleton<IClientBroker, StreamClientBroker>();

            return services;
        }
    }
}