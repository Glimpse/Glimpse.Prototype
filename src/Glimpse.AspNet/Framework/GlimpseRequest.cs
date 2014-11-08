using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Glimpse.Framework;

namespace Glimpse.AspNet.Framework
{
    public class GlimpseRequest : IHttpRequest
    {
        private readonly HttpRequest _request;

        public GlimpseRequest(HttpRequest request)
        {
            _request = request;
        }

        public string Accept 
        {
            get { return _request.Accept; }
        }

        public string Method
        {
            get { return _request.Method; }
        }

        public string RemoteIpAddress
        {
            get
            {
                //return _request.RemoteIpAddress;

                // TODO: Need to fix
                throw new NotImplementedException("Not supported yet");
            }
        }

        public int? RemotePort
        {
            get
            {
                //return _request.RemotePort;

                // TODO: Need to fix
                throw new NotImplementedException("Not supported yet");
            }
        }

        public string Scheme
        {
            get { return _request.Scheme; }
        }

        public string Host
        {
            get { return _request.Host.Value; }
        }

        public string PathBase
        {
            get { return _request.PathBase.Value; }
        }

        public string Path
        {
            get { return _request.Path.Value; }
        }

        public string QueryString
        {
            get { return _request.QueryString.Value; }
        }

        public string GetQueryString(string key)
        {
            return _request.Query[key];
        }

        public IList<string> GetQueryStringValues(string key)
        {
            return _request.Query.GetValues(key);
        }

        public string GetHeader(string key)
        {
            return _request.Headers[key];
        }

        public IList<string> GetHeaderValues(string key)
        {
            return _request.Headers.GetValues(key);
        }

        public IEnumerable<string> HeaderKeys
        {
            get { return _request.Headers.Keys; }
        }

        public string GetCookie(string key)
        {
            return _request.Cookies[key];
        }

        public IList<string> GetCookieValues(string key)
        {
            return _request.Cookies.GetValues(key);
        }

        public IEnumerable<string> CookieKeys
        {
            get { return _request.Cookies.Keys; }
        }
    }
}