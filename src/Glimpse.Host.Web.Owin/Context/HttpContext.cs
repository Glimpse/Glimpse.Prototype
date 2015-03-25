using System;
using System.Collections.Generic;
using System.Security.Claims;
using Glimpse.Web;

namespace Glimpse.Host.Web.Owin
{
    public class HttpContext : IHttpContext
    {
        private readonly string _itemsKey = "glimpse.RequestStore";
        private readonly Microsoft.Owin.IOwinContext _context;
        private readonly IHttpRequest _request;
        private readonly IHttpResponse _response;
        private readonly IServiceProvider _globalServices;
        private readonly IServiceProvider _localServices;
        private readonly ISettings _settings;

        public HttpContext(IDictionary<string, object> environement, IServiceProvider globalServices, IServiceProvider localServices, ISettings settings)
        {
            _context = new Microsoft.Owin.OwinContext(environement);
            _request = new HttpRequest(_context.Request);
            _response = new HttpResponse(_context.Response);
            _globalServices = globalServices;
            _localServices = localServices;
            _settings = settings;
        }

        public IHttpRequest Request
        {
            get { return _request;  }
        }

        public IHttpResponse Response
        {
            get { return _response; }
        }

        public IDictionary<string, object> Items
        {
            get
            {
                if (_context.Environment.ContainsKey(_itemsKey))
                {
                    return (IDictionary<string, object>)_context.Environment[_itemsKey];
                }

                var result = new Dictionary<string, object>();
                _context.Environment[_itemsKey] = result;

                return result;
            }
        }

        public ClaimsPrincipal User
        {
            get { return _context.Authentication.User; }
        }
        
        public IServiceProvider GlobalServices
        {
            get { return _globalServices; }
        }

        public IServiceProvider LocalServices
        {
            get { return _localServices; }
        }

        public ISettings Settings
        {
            get { return _settings; }
        }
    }
}