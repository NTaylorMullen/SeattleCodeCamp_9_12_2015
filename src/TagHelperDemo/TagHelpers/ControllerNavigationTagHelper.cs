using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace TagHelperDemo.TagHelpers
{
    [OutputElementHint("ul")]
    public class ControllerNavigationTagHelper : TagHelper
    {
        private readonly IUrlHelper _urlHelper;

        public ControllerNavigationTagHelper(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public Type ControllerType { get; set; }

        public string Exclude { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;

            var actionNames = ControllerType.GetTypeInfo().DeclaredMethods
                .Where(methodInfo => methodInfo.IsPublic)
                .Select(methodInfo => methodInfo.Name);

            var controllerName = ControllerType.Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
            }

            foreach (var action in actionNames)
            {
                if (!string.Equals(action, Exclude, StringComparison.OrdinalIgnoreCase))
                {
                    var displayName = action;

                    if (string.Equals(action, "Index", StringComparison.OrdinalIgnoreCase))
                    {
                        displayName = controllerName;
                    }

                    var liElement = new HtmlString($"<li><a href='{_urlHelper.Action(action, controllerName)}'>{displayName}</a></li>");
                    output.Content.Append(liElement);
                }
            }
        }
    }
}
