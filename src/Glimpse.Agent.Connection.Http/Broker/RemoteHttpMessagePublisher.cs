using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public class RemoteHttpMessagePublisher : IMessagePublisher, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;

        public RemoteHttpMessagePublisher()
        {
            _httpHandler = new HttpClientHandler();
            _httpClient = new HttpClient(_httpHandler);
        }

        public void PublishMessage(IMessage message)
        {
            var content = new StringContent("Hello");


            // TODO: Try shifting to async and await
            _httpClient.PostAsync("http://localhost:15999/Glimpse/Agent", content)
                .ContinueWith(requestTask =>
                    {
                        // Get HTTP response from completed task.
                        HttpResponseMessage response = requestTask.Result;

                        // Check that response was successful or throw exception
                        response.EnsureSuccessStatusCode();

                        // Read response asynchronously as JsonValue and write out top facts for each country
                        var result = response.Content.ReadAsStringAsync().Result;
                    });
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _httpHandler.Dispose();
        }
    }
}