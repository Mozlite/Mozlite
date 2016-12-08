using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.TagHelpers.Toolbars;
using Mozlite.Utils;

namespace Mozlite.TagHelpers.ListViews
{
    /// <summary>
    /// �б�ͷ����
    /// </summary>
    [HtmlTargetElement("item", ParentTag = "list-header")]
    public class ListHeaderItemTagHelper : ViewContextableTagHelper
    {
        [HtmlAttributeName("x-mode")]
        public ItemMode Mode { get; set; }

        [HtmlAttributeName("x-sorter")]
        public object Sorter { get; set; }

        [HtmlAttributeName("x-query")]
        public ISortable Query { get; set; }

        /// <summary>
        /// �첽���ʲ����ֵ�ǰ��ǩʵ����
        /// </summary>
        /// <param name="context">��ǰHTML��ǩ�����ģ�������ǰHTML�����Ϣ��</param>
        /// <param name="output">��ǰ��ǩ���ʵ�������ڳ��ֱ�ǩ�����Ϣ��</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            var builder = new TagBuilder("li");
            builder.AddCssClass("col");
            if ((Mode & ItemMode.First)==ItemMode.First)
            {
                builder.AddCssClass("first-col");
                output.Content.AppendHtml("<input type=\"checkbox\" />");//ȫѡ
            }
            if ((Mode & ItemMode.Last) == ItemMode.Last)
            {
                builder.AddCssClass("last-col");
            }
            output.Content.AppendHtml("<span class=\"text\">");
            output.Content.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml("</span>");
            if (Sorter != null)//����
            {
                var query = Query ?? ViewContext.ViewData.Model as ISortable;
                if (query != null)
                {
                    output.Content.AppendHtml($"<span class=\"fa fa-long-arrow-{(query.IsDesc ? "down" : "up")}\"></span>");
                    builder.AddCssClass("sortable");
                    if (query.Sorter.ToString() == Sorter.ToString())
                    {
                        builder.AddCssClass("active");
                        builder.MergeAttribute("_href", ViewContext.SetQueryUrl(qs =>
                        {
                            qs["Sorter"] = Sorter;
                            qs["IsDesc"] = !query.IsDesc;
                        }));
                    }
                    else
                        builder.MergeAttribute("_href", ViewContext.SetQueryUrl("Sorter", Sorter));
                }
            }
            output.MergeAttributes(builder);
        }
    }
}