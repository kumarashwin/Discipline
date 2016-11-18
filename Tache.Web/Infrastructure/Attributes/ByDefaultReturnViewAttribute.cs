using System;
using System.Linq;
using System.Web.Mvc;

namespace Tache.Infrastructure.Attributes {
    public class ByDefaultReturnViewAttribute : FilterAttribute, IActionFilter {
        public void OnActionExecuted(ActionExecutedContext filterContext) {
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

        public void OnActionExecuting(ActionExecutingContext filterContext) { }
    }
}