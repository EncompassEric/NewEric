namespace Web.Infrastructure.Extentions
{
    using System;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using ViewModel;

    public static class ExtendHtmlHelper
    {
        public static MvcHtmlString RenderTextBox<TEntity, TProperty>(this HtmlHelper<TEntity> htmlHelper, Expression<Func<TEntity, TProperty>> expression)
        {
            return RenderFormElement(htmlHelper, expression, htmlHelper.TextBoxFor);
        }

        public static MvcHtmlString RenderPassword<TEntity, TProperty>(this HtmlHelper<TEntity> htmlHelper,
                                                                       Expression<Func<TEntity, TProperty>> expression)
        {
            return RenderFormElement(htmlHelper, expression, htmlHelper.PasswordFor);
        }

        public static MvcForm RenderForm(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method)
        {
            return htmlHelper.BeginForm(actionName, controllerName, method, new { @class = "pure-form pure-form-aligned" });
        }

        public static MvcHtmlString RenderSubmitButton(this HtmlHelper htmlHelper)
        {
            StringBuilder sb = new StringBuilder(@"<div class='pure-controls'>
														<button type='submit' class='pure-button pure-button-primary'>Submit</button>
													</div>");

            MvcHtmlString html = MvcHtmlString.Create(sb.ToString());

            return html;
        }

        private static MvcHtmlString RenderFormElement<TEntity, TProperty>(HtmlHelper<TEntity> htmlHelper, Expression<Func<TEntity, TProperty>> expression, Func<Expression<Func<TEntity, TProperty>>, MvcHtmlString> input)
        {
            MvcHtmlString html = default(MvcHtmlString);

            StringBuilder sb = new StringBuilder("<div class='pure-control-group'>");

            MvcHtmlString label = LabelExtensions.LabelFor(htmlHelper, expression);

            MvcHtmlString textbox = input(expression);

            MvcHtmlString validate = ValidationExtensions.ValidationMessageFor(htmlHelper, expression);

            sb.Append(label.ToHtmlString());
            sb.Append(textbox.ToHtmlString());
            sb.Append(validate.ToHtmlString());

            sb.Append("</div>");

            html = MvcHtmlString.Create(sb.ToString());

            return html;
        }

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl, string[] searchKeys)
        {
            if (pagingInfo.TotalPages > 0)
            {
                string url = string.Empty;

                StringBuilder result = new StringBuilder();
                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("pagination  pagination-centered");
                TagBuilder ul = new TagBuilder("ul");

                TagBuilder litag = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.InnerHtml = "上一页";

                int prev = pagingInfo.CurrentPage - 1;
                prev = prev < 1 ? 1 : prev;

                url = pageUrl(prev) + GenerateSearchQuery(html, searchKeys);

                tag.MergeAttribute("href", url);
                litag.InnerHtml = tag.ToString();

                result.Append(litag);

                for (int i = 1; i <= pagingInfo.TotalPages; i++)
                {
                    litag = new TagBuilder("li");

                    tag = new TagBuilder("a");

                    tag.AddCssClass("pure-button");

                    url = pageUrl(i) + GenerateSearchQuery(html, searchKeys);
                    tag.MergeAttribute("href", url);
                    tag.InnerHtml = i.ToString();
                    if (i == pagingInfo.CurrentPage)
                        tag.AddCssClass("pure-button-selected");

                    litag.InnerHtml = tag.ToString();

                    result.Append(litag);
                }

                litag = new TagBuilder("li");

                int next = pagingInfo.CurrentPage + 1;
                next = next > pagingInfo.TotalPages ? pagingInfo.TotalPages : next;

                tag = new TagBuilder("a");
                url = pageUrl(next) + GenerateSearchQuery(html, searchKeys);
                tag.MergeAttribute("href", url);
                tag.InnerHtml = "下一页";
                litag.InnerHtml = tag.ToString();

                result.Append(litag);

                ul.InnerHtml = result.ToString();
                div.InnerHtml = ul.ToString();
                return MvcHtmlString.Create(div.ToString());
            }
            else
            {
                return MvcHtmlString.Empty;
            }
        }

        public static MvcHtmlString AdminPageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl, string[] searchKeys)
        {
            if (pagingInfo.TotalPages > 0)
            {
                string url = string.Empty;

                StringBuilder result = new StringBuilder();
                TagBuilder ul = new TagBuilder("ul");
                ul.AddCssClass("ui_page");
                TagBuilder litag = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.InnerHtml = "上一页";

                int prev = pagingInfo.CurrentPage - 1;
                prev = prev < 1 ? 1 : prev;

                url = pageUrl(prev) + GenerateSearchQuery(html, searchKeys);

                tag.MergeAttribute("href", url);
                litag.InnerHtml = tag.ToString();

                result.Append(litag);

                for (int i = 1; i <= pagingInfo.TotalPages; i++)
                {
                    litag = new TagBuilder("li");
                    if (i == pagingInfo.CurrentPage)
                        litag.AddCssClass("current");
                    tag = new TagBuilder("a");

                    url = pageUrl(i) + GenerateSearchQuery(html, searchKeys);
                    tag.MergeAttribute("href", url);
                    tag.InnerHtml = i.ToString();


                    litag.InnerHtml = tag.ToString();

                    result.Append(litag);
                }

                litag = new TagBuilder("li");

                int next = pagingInfo.CurrentPage + 1;
                next = next > pagingInfo.TotalPages ? pagingInfo.TotalPages : next;

                tag = new TagBuilder("a");
                url = pageUrl(next) + GenerateSearchQuery(html, searchKeys);
                tag.MergeAttribute("href", url);
                tag.InnerHtml = "下一页";
                litag.InnerHtml = tag.ToString();

                result.Append(litag);

                ul.InnerHtml = result.ToString();
                return MvcHtmlString.Create(ul.ToString());
            }
            else
            {
                return MvcHtmlString.Empty;
            }
        }

        public static MvcHtmlString AdminOrderLinks(this HtmlHelper html, PagingInfo pagingInfo, string columnName, string orderFiled, string defaultOrderFiled, Func<int, string> pageUrl, string[] searchKeys)
        {
            if (pagingInfo.TotalPages > 0)
            {
                string url = string.Empty;
                TagBuilder tag = new TagBuilder("a");
                int prev = pagingInfo.CurrentPage - 1;
                prev = prev < 1 ? 1 : prev;

                string value = html.ViewContext.HttpContext.Request["sort"];


                if (String.IsNullOrEmpty(value))
                {
                    tag.InnerHtml = columnName;
                    value = string.Format("&sort={0}", orderFiled + " Descending");
                }
                else
                {
                    if (value.Contains(orderFiled))
                    {
                        if (value.Contains("Descending"))
                        {
                            tag.InnerHtml = value.Contains(orderFiled) ? columnName + "down" : columnName;
                            value = string.Format("&sort={0}", orderFiled);
                        }
                        else
                        {
                            tag.InnerHtml = value.Contains(orderFiled) ? columnName + "up" : columnName;
                            value = string.Format("&sort={0}", defaultOrderFiled + " Descending");
                        }
                    }
                    else
                    {
                        tag.InnerHtml = columnName;
                        value = string.Format("&sort={0}", orderFiled + " Descending");
                    }
                }

                url = pageUrl(prev) + value + GenerateSearchQuery(html, searchKeys);

                tag.MergeAttribute("href", url);

                return MvcHtmlString.Create(tag.ToString());
            }
            else
            {
                return MvcHtmlString.Empty;
            }
        }

        private static string GenerateSearchQuery(HtmlHelper helper, string[] searchKeys)
        {
            StringBuilder sb = new StringBuilder();

            if (searchKeys != null && searchKeys.Length > 0)
            {
                foreach (var searchKey in searchKeys)
                {
                    sb.Append("&");

                    string value = helper.ViewContext.HttpContext.Request[searchKey];

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        sb.AppendFormat("{0}={1}", searchKey, value);
                    }
                }
            }

            if (sb.Length == 1)
            {
                sb.Remove(0, 1);
            }

            return sb.ToString();
        }
    }
}