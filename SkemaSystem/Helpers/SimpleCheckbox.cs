using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SkemaSystem
{
    public static class Helpers
    {
        public static MvcHtmlString SimpleCheckbox(this HtmlHelper htmlHelper, string name)
        {
            return SimpleCheckbox(htmlHelper, name, false, "true", null);
        }

        public static MvcHtmlString SimpleCheckbox(this HtmlHelper htmlHelper, string name, bool isChecked)
        {
            return SimpleCheckbox(htmlHelper, name, isChecked, "true", null);
        }

        public static MvcHtmlString SimpleCheckbox(this HtmlHelper htmlHelper, string name, bool isChecked, string value)
        {
            return SimpleCheckbox(htmlHelper, name, isChecked, value, null);
        }

        public static MvcHtmlString SimpleCheckbox(this HtmlHelper htmlHelper, string name, bool isChecked, string value, IDictionary<string, object> htmlAttributes)
        {
            var builder = new TagBuilder("input");

            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes.Remove("checked");

            builder.MergeAttribute("type", "checkbox");
            builder.MergeAttribute("name", name);
            builder.MergeAttribute("value", value);
            if (isChecked)
            {
                builder.MergeAttribute("checked", "checked");
            }
            builder.MergeAttributes(attributes);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}