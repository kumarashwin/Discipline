using System;
using System.Linq;
using System.Web.Mvc;

namespace Tache.Infrastructure.Attributes {
    public class ByDefaultReturnViewAttribute : FilterAttribute, IActionFilter {
        public void OnActionExecuted(ActionExecutedContext filterContext) {
            if (!filterContext.HttpContext.Request.AcceptTypes.Contains("application/json")) {
                filterContext.Result = new ViewResult {
                    ViewName = filterContext.ActionDescriptor.ActionName,
                    ViewData = new ViewDataDictionary { Model = ((ContentResult)filterContext.Result).Content }
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) { }
    }
}