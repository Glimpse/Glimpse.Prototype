using Glimpse.Web;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Server
{
    public class HttpClientChannelResource : IRequestHandler
    {
        public bool WillHandle(IHttpContext context)
        {
            return context.Request.Path == "/Glimpse/Data/History";
        }

        public async Task Handle(IHttpContext context)
        {
            var response = context.Response;

            response.SetHeader("Content-Type", "application/json");

            var content = new StringBuilder();
            content.Append("[");
            content.Append("{\"Type\":\"Glimpse.Agent.Web.BeginRequestMessage\",\"Payload\":\"{\\\"uri\\\":\\\"http://localhost:15999/\\\",\\\"id\\\":\\\"eb84b12a-a8a0-4409-a3e1-f933c6a6178a\\\",\\\"time\\\":\\\"2015-01-28T15:34:17.8300565-08:00\\\",\\\"timeLong\\\":635580560578300565}\",\"Context\":{\"Id\":\"9bd1a331-6ce6-49fe-8230-dc1398c58951\",\"Type\":\"Request\"}},");
            content.Append("{\"Type\":\"Glimpse.Agent.Web.EndRequestMessage\",\"Payload\":\"{\\\"uri\\\":\\\"http://localhost:15999/\\\",\\\"id\\\":\\\"455fca26-e3a2-4325-8616-33bc1e557ee9\\\",\\\"time\\\":\\\"2015-01-28T15:34:17.8300565-08:00\\\",\\\"timeLong\\\":635580560578300565}\",\"Context\":{\"Id\":\"9bd1a331-6ce6-49fe-8230-dc1398c58951\",\"Type\":\"Request\"}},");
            content.Append("{\"Type\":\"browser.rum\",\"Payload\":\"{\\\"loadEventEnd\\\":1422488057999,\\\"loadEventStart\\\":1422488057994,\\\"domComplete\\\":1422488057994,\\\"domContentLoadedEventEnd\\\":1422488057974,\\\"domContentLoadedEventStart\\\":1422488057973,\\\"domInteractive\\\":1422488057973,\\\"domLoading\\\":1422488057857,\\\"responseEnd\\\":1422488057831,\\\"responseStart\\\":1422488057831,\\\"requestStart\\\":1422488057826,\\\"secureConnectionStart\\\":0,\\\"connectEnd\\\":1422488057823,\\\"connectStart\\\":1422488057823,\\\"domainLookupEnd\\\":1422488057823,\\\"domainLookupStart\\\":1422488057823,\\\"fetchStart\\\":1422488057823,\\\"redirectEnd\\\":0,\\\"redirectStart\\\":0,\\\"unloadEventEnd\\\":1422488057852,\\\"unloadEventStart\\\":1422488057833,\\\"navigationStart\\\":1422488057823,\\\"id\\\":\\\"e3599391-639a-4809-b7d3-10b286f59d3e\\\",\\\"time\\\":\\\"2015-01-28T23:34:18.102Z\\\",\\\"uri\\\":\\\"http://localhost:15999/\\\"}\",\"Context\":{\"Id\":\"9bd1a331-6ce6-49fe-8230-dc1398c58951\",\"Type\":\"Request\"}}");
            content.Append("]");

            var data = Encoding.UTF8.GetBytes(content.ToString());
            await response.WriteAsync(data);
        }
    }
}