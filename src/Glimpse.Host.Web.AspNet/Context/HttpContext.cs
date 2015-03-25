using System;
using System.Collections.Generic;
using System.Security.Claims;
using Glimpse.Web;

namespace Glimpse.Host.Web.AspNet
{
    public class HttpContext : IHttpContext
    {
        private readonly string _itemsKey = "glimpse.RequestStore";
        private readonly Microsoft.AspNet.Http.HttpContext _context;
        private readonly IHttpRequest _request;
        private readonly IHttpResponse _response;
        private readonly ISettings _settings;

        public HttpContext(Microsoft.AspNet.Http.HttpContext context, ISettings settings)
        {
            _context = context;
            _request = new HttpRequest(context.Request, context.GetFeature<Microsoft.AspNet.Http.Interfaces.IHttpConnectionFeature>());
            _response = new HttpResponse(context.Response);
            _settings = settings;
        }

        public IHttpRequest Request
        {
            get { return _request; }
        }

        public IHttpResponse Response
        {
            get { return _response; }
        }

        public IDictionary<string, object> Items
        {
            get
            {
                if (_context.Items.ContainsKey(_itemsKey))
                {
                    return (IDictionary<string, object>)_context.Items[_itemsKey];
                }

                var result = new Dictionary<string, object>();
                _context.Items[_itemsKey] = result;

                return result;
            }
        }
        
        public ClaimsPrincipal User
        {
            get { return _context.User; }
        }

        public IServiceProvider GlobalServices
        {
            get { return _context.ApplicationServices; }
        }

        public IServiceProvider LocalServices
        {
            get { return _context.RequestServices; }
        }

        public ISettings Settings
        {
            get { return _settings; }
        }
    }
}