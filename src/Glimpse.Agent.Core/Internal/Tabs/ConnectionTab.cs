//using Glimpse.Agent.Inspectors;
//using Microsoft.AspNet.Http;

//namespace Glimpse.Agent.AspNet.Internal.Tabs
//{
//    // TODO: Delete me. This tab isn't intended to stick around, it's just a sample of a Tab.
//    public class ConnectionTab : Tab
//    {
//        public override string Name => "Connection";

//        public override object GetData(HttpContext context)
//        {
//            var connection = context.Connection;
//            return new
//            {
//                ClientCertificate = connection.ClientCertificate?.ToString(),
//                connection.IsLocal,
//                LocalIpAddress = connection.LocalIpAddress.ToString(),
//                connection.LocalPort,
//                RemoteIpAddress = connection.RemoteIpAddress.ToString(),
//                connection.RemotePort
//            };
//        }

//        public override Execute ExecuteWhen => Execute.BeforeResponse;
//    }
//}
