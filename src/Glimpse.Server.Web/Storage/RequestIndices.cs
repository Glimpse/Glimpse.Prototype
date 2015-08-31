using System;
using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Server.Web
{
    public class RequestIndices
    {
        private readonly List<string> _tags;

        public RequestIndices(IMessage message)
        {
            Id = message.Context.Id;
            _tags = new List<string>();

            ParseAndUpdateIndicesFor(message);
        }

        public double? Duration { get; private set; }

        public string Url { get; private set; }

        public string Method { get; private set; }

        public int? StatusCode { get; private set; }

        public Guid Id { get; }

        public DateTime? DateTime { get; private set; }

        public IEnumerable<string> Tags => _tags;

        public void Update(IMessage message)
        {
            if (message.Context.Id != Id)
                throw new ArgumentException($"Input Request ID '{message.Context.Id}' does not match existing request ID '{Id}'.");

            ParseAndUpdateIndicesFor(message);
        }

        private void ParseAndUpdateIndicesFor(IMessage message)
        {
            var indices = message.Indices;
            Duration = indices.GetValueOrDefault("request-duration") as double?;
            Url = indices.GetValueOrDefault("request-url") as string;
            Method = indices.GetValueOrDefault("request-method") as string;
            StatusCode = indices.GetValueOrDefault("request-statuscode") as int?;
            DateTime = indices.GetValueOrDefault("request-datetime") as DateTime?;

            var messageTags = indices.GetValueOrDefault("request-tags") as IEnumerable<string> ?? Enumerable.Empty<string>();
            _tags.AddRange(messageTags);
        }
    }
}