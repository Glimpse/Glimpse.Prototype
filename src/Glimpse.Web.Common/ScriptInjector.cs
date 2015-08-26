using System.Text;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Mvc.Rendering;

namespace Glimpse.Web.Common
{
    [TargetElement("body")]
    public class ScriptInjector : TagHelper
    {
        public override int Order => int.MaxValue;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var js = new StringBuilder();
            js.AppendLine("var link = document.createElement('a');");
            js.AppendLine("link.setAttribute('href', '/glimpseui/index.html');");
            js.AppendLine("link.setAttribute('target', '_blank');");
            js.AppendLine("link.text = 'Open Glimpse';");
            js.AppendLine("document.body.appendChild(link);");

            var tag = new TagBuilder("script")
            {
                InnerHtml = new StringHtmlContent(js.ToString())
            };

            output.PostContent.Append(tag.InnerHtml);
        }
    }
}
