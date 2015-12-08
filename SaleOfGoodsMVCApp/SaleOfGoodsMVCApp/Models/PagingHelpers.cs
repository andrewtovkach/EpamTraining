using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace SaleOfGoodsMVCApp.Models
{
    public static class PagingHelpers
    {
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string rawHtml, string action, 
            string controller, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            string holder = Guid.NewGuid().ToString();
            string anchor = ajaxHelper.ActionLink(holder, action, controller, routeValues, ajaxOptions, htmlAttributes).ToString();
            return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));
        }
    }
}
