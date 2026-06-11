using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "show-when-gt, for")]
    public class SelectiveTagHelper : TagHelper
    {
        public decimal ShowWhenGt { get; set; }
        public ModelExpression For { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For.Model.GetType() == typeof(decimal) && (decimal)For.Model <= ShowWhenGt)
            {
                output.SuppressOutput();//Tag helpers can be used to prevent an element from being included in the HTML response
            }
        }
    }
}
