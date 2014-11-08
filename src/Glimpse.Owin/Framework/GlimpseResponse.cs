using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Glimpse.Framework;

namespace Glimpse.Owin.Framework
{
    public class GlimpseResponse : IHttpResponse
    {
        private readonly OwinResponse _response;

        public GlimpseResponse(OwinResponse response)
        {
            _response = response;
        }

        public string ContentType
        {
            get { return _response.ContentType; }
        }

        public int StatusCode
        {
            get { return _response.StatusCode; }
        }

        public string GetHeader(string key)
        {
            return _response.Headers[key];
        }

        public IList<string> GetHeaderValues(string key)
        {
            return _response.Headers.GetValues(key);
        }

        public void SetHeaderValues(string key, IList<string> values)
        {
            //_response.Headers.SetValues(key, values);

            // TODO: Need to fix
            throw new NotImplementedException("Not supported yet");
        }

        public void SetHeader(string key, string values)
        {
            _response.Headers.Set(key, values);
        }

        public IEnumerable<string> HeaderKeys
        {
            get { return _response.Headers.Keys; }
        }

        public void Append(string key, string value)
        {
            _response.Cookies.Append(key, value);
        }

        public void Append(string key, string value, string domain, string path, DateTime? expires, bool secure, bool httpOnly)
        {
            _response.Cookies.Append(key, value, new CookieOptions { Domain = domain, Path = path, Expires = expires, Secure = secure, HttpOnly = httpOnly });
        }

        public void Delete(string key)
        {
            _response.Cookies.Delete(key);
        }

        public void Delete(string key, string domain, string path, DateTime? expires, bool secure, bool httpOnly)
        {
            _response.Cookies.Delete(key, new CookieOptions { Domain = domain, Path = path, Expires = expires, Secure = secure, HttpOnly = httpOnly });
        }

        public Task WriteAsync(string text)
        {
            return _response.WriteAsync(text);
        }

        public Task WriteAsync(byte[] data)
        {
            return _response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}