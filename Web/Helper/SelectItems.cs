using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Infrastructure.Helper;

namespace Web.Helper
{
    public class SelectItems
    {
        public static SelectList BuyTimeSelectList()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem {Text = "立即", Value = "0"},
                new SelectListItem {Text = "一天", Value = "1"},
                new SelectListItem {Text = "两天", Value = "2"},
                new SelectListItem {Text = "三天", Value = "3"},
                new SelectListItem {Text = "四天", Value = "4"},
                new SelectListItem {Text = "五天", Value = "5"},
                new SelectListItem {Text = "六天", Value = "6"},
                new SelectListItem {Text = "七天", Value = "7"}
            };

            return new SelectList(items, "Value", "Text");
        }
    }
}