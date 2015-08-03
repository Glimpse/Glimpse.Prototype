using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Mvc.Rendering;

namespace Glimpse.Host.Web.AspNet
{
    [TargetElement("body")]
    public class ScriptInjector : TagHelper
    {
        public override int Order => int.MaxValue;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var tag = new TagBuilder("script")
            {
                InnerHtml = "console.log('Yey!');"
            };

            output.PostContent.Append(tag.ToString());
        }
    }
}
