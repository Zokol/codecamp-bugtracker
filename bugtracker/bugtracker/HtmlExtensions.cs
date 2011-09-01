
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace bugtracker
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString ActionLinkWithColumnOrder(this HtmlHelper helper,
            string columnName,string action,string currentColumn,bool currentOrder)
        {
            object routeValues;
            object htmlAttributes = null;
            if (columnName == currentColumn)
            {
                routeValues = new { sortColumn = columnName, asc = !currentOrder };
                htmlAttributes = new { @class = currentOrder ? "sort_asc" : "sort_desc" };
            }
            else
            {
                routeValues = new { sortColumn = columnName };
            }

            return helper.ActionLink(columnName, action, routeValues, htmlAttributes);
        }
    }
}