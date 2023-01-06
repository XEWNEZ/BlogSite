using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminBlog.Filter
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int? userId = filterContext.HttpContext.Session.GetInt32("id");
            if (!userId.HasValue)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"action", "Login"},
                    {"controller", "Account"}
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}