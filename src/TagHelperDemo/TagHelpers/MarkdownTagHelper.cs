using System.Threading.Tasks;
using MarkdownSharp;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace TagHelperDemo.TagHelpers
{
    [TargetElement("markdown")]
    [TargetElement(Attributes = "markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        private static readonly Markdown MarkdownParser = new Markdown();

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await context.GetChildContentAsync();
            var parsedContent = MarkdownParser.Transform(childContent.GetContent());

            output.Content.SetContent(new HtmlString(parsedContent));
        }
    }
}
