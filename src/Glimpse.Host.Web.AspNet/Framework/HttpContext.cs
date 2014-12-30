using System;
using System.Collections.Generic;
using Glimpse.Web;

namespace Glimpse.Host.Web.AspNet
{
    public class HttpContext : IHttpContext
    {
        private readonly string _itemsKey = "glimpse.RequestStore";
        private readonly Microsoft.AspNet.Http.HttpContext _context;
        private readonly IHttpRequest _request;
        private readonly IHttpResponse _response;

        public HttpContext(Microsoft.AspNet.Http.HttpContext context)
        {
            _context = context;
            _request = new HttpRequest(context.Request);
            _response = new HttpResponse(context.Response);
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

        public IServiceProvider ApplicationServices
        {
            get { return _context.ApplicationServices; }
        }
    }
}