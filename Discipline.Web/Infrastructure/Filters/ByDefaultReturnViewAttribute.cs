using System;
using System.Linq;
using System.Web.Mvc;

namespace Discipline.Web.Infrastructure.Filters {
    public class ByDefaultReturnViewAttribute : ActionFilterAttribute {
        /// <summary>
        /// As long as no exception has been raised, this filter will wrap the
        /// json string in the ContentResult returned by the Action into a View
        /// after copying the ViewData. This is therefore useful for the initial
        /// request with the default html content-type.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            if(filterContext.Exception == null) {
                if (!filterContext.HttpContext.Request.AcceptTypes.Contains("application/json")) {
                    filterContext.Controller.ViewData.Model = ((ContentResult)filterContext.Result).Content;
                    filterContext.Result = new ViewResult {
                        ViewName = filterContext.ActionDescriptor.ActionName,
                        ViewData = filterContext.Controller.ViewData
                    };
                }
            }
        }
    }
}