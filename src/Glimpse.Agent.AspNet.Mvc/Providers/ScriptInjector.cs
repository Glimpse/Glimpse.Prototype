using System;
using Glimpse.Web;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace Glimpse.Agent.AspNet.Mvc
{
    [HtmlTargetElement("body")]
    public class ScriptInjector : TagHelper
    {
        private readonly Guid _requestId;
        private readonly ScriptOptions _scriptOptions;

        public ScriptInjector(IContextData<MessageContext> context, IScriptOptionsProvider scriptOptionsProvider)
        {
            _requestId = context.Value.Id;
            _scriptOptions = scriptOptionsProvider.BuildInstance();
        }

        public override int Order => int.MaxValue;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var tag = new TagBuilder("script");
            tag.Attributes.Add("data-request-id", _requestId.ToString());
            tag.Attributes.Add("src", _scriptOptions.HudClientScriptUri);

            output.PostContent.SetContent(tag);
        }
    }
}
