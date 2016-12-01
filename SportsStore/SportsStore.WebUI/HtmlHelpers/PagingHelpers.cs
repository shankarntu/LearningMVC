using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString pageLinks(this HtmlHelper html, PagingInfo pageInfo, Func<int, string>pageUrl) {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i < pageInfo.TotalPages+1; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
               // tag.MergeAttribute("type", "button");
                tag.InnerHtml = i.ToString();

                if (i == pageInfo.CurrentPage)
                {
                    tag.AddCssClass("btn btn-default btn btn-primary");
                    tag.AddCssClass("selected");
                }
                tag.AddCssClass("btn btn-default btn btn-secondary");
                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}