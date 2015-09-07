using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace TagHelperDemo.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string MailTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var domain = MailTo + "@example.com";
            output.Content.SetContent(domain);
            output.Attributes["href"] = "mailto:" + domain;
            output.PreElement.SetContent(new HtmlString($"<strong>{MailTo}:</strong> "));
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
