using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public interface IHttpResponse
    {
        string ContentType { get; }

        int StatusCode { get; }

        string GetHeader(string key);

        IList<string> GetHeaderValues(string key);

        void SetHeaderValues(string key, IList<string> values);

        void SetHeader(string key, string values);

        IEnumerable<string> HeaderKeys { get; }

        void Append(string key, string value);

        void Append(string key, string value, string domain, string path, DateTime? expires, bool secure, bool httpOnly);

        void Delete(string key);

        void Delete(string key, string domain, string path, DateTime? expires, bool secure, bool httpOnly);

        Task WriteAsync(string text);

        Task WriteAsync(byte[] data);
    }
}