using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class RemoteHttpMessagePublisher : IMessagePublisher, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;
        private readonly IMessageConverter _messageConverter;

        public RemoteHttpMessagePublisher(IMessageConverter messageConverter)
        {
            _httpHandler = new HttpClientHandler();
            _httpClient = new HttpClient(_httpHandler);
            _messageConverter = messageConverter;
        }

        public async Task PublishMessage(IMessage message)
        {
            // TODO: Needs error handelling
            // TODO: Find out what happened to System.Net.Http.Formmating - PostAsJsonAsync

            var newMessage = _messageConverter.ConvertMessage(message);

            var response = await _httpClient.PostAsJsonAsync("http://localhost:15999/Glimpse/Agent", newMessage);
  
            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();

            // Read response asynchronously as JsonValue and write out top facts for each country
            var result = await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _httpHandler.Dispose();
        }
    }
}