using System;
using System.Collections.Concurrent;
using Glimpse.Agent.Web.Framework;
using Glimpse.Agent.Web.Message;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;

namespace Glimpse.Agent.Web.Inspectors
{
    public class UserInspector : Inspector
    {
        private readonly IAgentBroker _broker;
        private const string GlimpseCookie = ".Glimpse.Session"; // TODO: Move this somewhere configurable
        private readonly ConcurrentDictionary<string, string> _gravatarCache = new ConcurrentDictionary<string, string>(); 
        private readonly ConcurrentDictionary<string, string> _crcCache = new ConcurrentDictionary<string, string>(); 

        public UserInspector(IAgentBroker broker)
        {
            _broker = broker;
        }

        public override void After(HttpContext context)
        {
            var user = context.User;
            var userId = user.FindFirst("sub")?.Value ?? user.FindFirst("NameIdentifier")?.Value ?? context.Request.Cookies[GlimpseCookie];

            if (string.IsNullOrWhiteSpace(userId))
            {
#warning TODO: Use Glimpse RequestId
                userId = Guid.NewGuid().ToString(); // TODO: This should be the Glimpse request ID

                var response = context.Features.Get<IHttpResponseFeature>();
                if (!response.HasStarted)
                {
                    context.Response.Cookies.Append(GlimpseCookie, userId);
                }
            }

            var username = user.FindFirst("name")?.Value ?? _crcCache.GetOrAdd(userId, id => id.Crc16().ToUpper());
            var email = user.FindFirst("email")?.Value;
            var pic = user.FindFirst("picture")?.Value ?? _gravatarCache.GetOrAdd(email ?? userId, GenerateGravatarUrl);

            _broker.SendMessage(new UserIdentification(userId, username, email, pic));
        }

        private string GenerateGravatarUrl(string email)
        {
            var preppedEmail = email.Trim().ToLower();
            var hash = preppedEmail.Md5();
            return $"https://www.gravatar.com/avatar/{hash}.jpg?d=identicon";
        }
    }
}