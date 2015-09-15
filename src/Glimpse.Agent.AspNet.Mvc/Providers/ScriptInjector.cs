using System;
using System.Text;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Mvc.Rendering;

namespace Glimpse.Agent.AspNet.Mvc
{
    [HtmlTargetElement("body")]
    public class ScriptInjector : TagHelper
    {
        private readonly Guid _requestId;

        public ScriptInjector(IContextData<MessageContext> context)
        {
            _requestId = context.Value.Id;
        }

        public override int Order => int.MaxValue;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var js = @"
var script = document.querySelector('script[data-request-id]'),
link = document.createElement('a'),
requestId = script.dataset.requestId;
link.setAttribute('href', '/glimpse/client/index.html?id=' + requestId);
link.setAttribute('target', '_blank');
link.text = 'Open Glimpse';
document.body.appendChild(link);";

            var tag = new TagBuilder("script")
            {
                InnerHtml = new HtmlString(js)
            };

            tag.Attributes.Add("data-request-id", _requestId.ToString());

            output.PostContent.Append(tag);
        }
    }
}
