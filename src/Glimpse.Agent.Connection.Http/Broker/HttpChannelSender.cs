using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class HttpChannelSender : IChannelSender, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;

        public HttpChannelSender()
        {
            _httpHandler = new HttpClientHandler();
            _httpClient = new HttpClient(_httpHandler);
        }

        public async void PublishMessage(IMessage message)
        {
            // TODO: Needs error handelling
            // TODO: Find out what happened to System.Net.Http.Formmating - PostAsJsonAsync
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:15999/Glimpse/Agent", message);

                // Check that response was successful or throw exception
                response.EnsureSuccessStatusCode();

                // Read response asynchronously as JsonValue and write out top facts for each country
                var result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                // TODO: Bad thing happened
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _httpHandler.Dispose();
        }
    }
}