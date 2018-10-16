using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ServiceCenterWeb.Models;

namespace ServiceCenterWeb.Utils
{
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            // Набор ссылок будет представлять список ul
            var tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            // Формируем три ссылки - на текущую, предыдущую и следующую
            var currentItem = CreateTag(PageModel.PageNumber, urlHelper);

            // Создаем ссылку на предыдущую страницу, если она есть
            if (PageModel.HasPreviousPage) {
                var prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper);
                tag.InnerHtml.AppendHtml(prevItem);
            } // if

            tag.InnerHtml.AppendHtml(currentItem);

            // Создаем ссылку на следующую страницу, если она есть
            if (PageModel.HasNextPage) {
                var nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper);
                tag.InnerHtml.AppendHtml(nextItem);
            } // if

            output.Content.AppendHtml(tag);
        }

        private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            var item = new TagBuilder("li");
            var link = new TagBuilder("a");

            if (pageNumber == PageModel.PageNumber) {
                item.AddCssClass("active");
            } else {
                PageUrlValues["page"] = pageNumber;
                link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            } // if

            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);

            return item;
        }
    }
}
