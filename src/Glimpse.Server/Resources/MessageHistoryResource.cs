using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glimpse.Server.Web.Extensions;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Web
{
    public class MessageHistoryResource : IResource
    {
        private readonly IStorage _store;
        private readonly IQueryRequests _requests;
        private readonly bool _canQueryRequests;

        public MessageHistoryResource(IStorage storage)
        {
            _store = storage;
            _requests = storage.As<IQueryRequests>();

            _canQueryRequests = storage.Supports<IQueryRequests>();
            if (_canQueryRequests)
                Parameters = new ResourceParameters(
                    ResourceParameter.Custom("dmin"),
                    ResourceParameter.Custom("dmax"),
                    ResourceParameter.Custom("url"),
                    ResourceParameter.Custom("methods"),
                    ResourceParameter.Custom("smin"),
                    ResourceParameter.Custom("smax"),
                    ResourceParameter.Custom("tags"),
                    ResourceParameter.Custom("before"),
                    ResourceParameter.Custom("user"),
                    ResourceParameter.Custom("types"));
            else
                Parameters = new ResourceParameters(ResourceParameter.Custom("types"));
            
        }

        // TODO: The parameter parsing in this method is brittle and will require more work
        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "application/json";

            IEnumerable<string> list;
            if (!_canQueryRequests)
            {
                list = await _store.RetrieveByType(parameters["types"].Split(','));
            }
            else
            {
                var filters = new RequestFilters();
                if (parameters.ContainsKey("dmin"))
                    filters.DurationMinimum = float.Parse(parameters["dmin"]);

                if (parameters.ContainsKey("dmax"))
                    filters.DurationMaximum = float.Parse(parameters["dmax"]);

                if (parameters.ContainsKey("url"))
                    filters.UrlContains = parameters["url"];

                if (parameters.ContainsKey("methods"))
                    filters.MethodList = parameters["methods"].Split(',');

                if (parameters.ContainsKey("smin"))
                    filters.StatusCodeMinimum = int.Parse(parameters["smin"]);

                if (parameters.ContainsKey("smax"))
                    filters.StatusCodeMaximum = int.Parse(parameters["smax"]);

                if (parameters.ContainsKey("tags"))
                    filters.TagList = parameters["tags"].Split(',');

                if (parameters.ContainsKey("user"))
                    filters.UserId = parameters["user"];

                if (parameters.ContainsKey("before"))
                    filters.RequesTimeBefore = DateTime.Parse(parameters["before"]);

                if (parameters.ContainsKey("types"))
                {
                    var types = parameters["types"].Split(',');
                    list = await _requests.Query(filters, types);
                }
                else
                {
                    list = await _requests.Query(filters);
                }
            }

            var sb = new StringBuilder("[");
            sb.Append(string.Join(",", list));
            sb.Append("]");
            var output = sb.ToString();

            await response.WriteAsync(output);
        }

        public string Name => "MessageHistory";
        
        public ResourceParameters Parameters { get; }

        public ResourceType Type => ResourceType.Client;
    }
}