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
            // TODO: Find out what happened to System.Net.Http.Formmating - PostAsJsonAsync

            _httpClient.PostAsJsonAsync("http://localhost:15999/Glimpse/Agent", message)
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