using System;
using Glimpse.Agent.Web.Framework;
using Glimpse.Agent.Web.Message;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;

namespace Glimpse.Agent.Web.Inspectors
{
    public class UserInspector : Inspector
    {
        private readonly IAgentBroker _broker;
        private const string GlimpseCookie = ".Glimpse.Session";

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

            var username = user.FindFirst("name")?.Value ?? userId.Crc16().ToUpper();
            var email = user.FindFirst("email")?.Value;
            var pic = user.FindFirst("picture")?.Value ?? GenerateGravatarUrl(email ?? userId);

            _broker.SendMessage(new UserIdentification(userId, username, email, pic));
        }

        private string GenerateGravatarUrl(string email)
        {
            var preppedEmail = email.Trim().ToLower();
            var hash = preppedEmail.Md5();
            return $"http://www.gravatar.com/avatar/{hash}.jpg?d=identicon";
        }
    }
}