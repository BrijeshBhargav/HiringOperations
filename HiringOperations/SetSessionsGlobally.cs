using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HiringOperations
{
    public class SetSessionsGlobally : ActionFilterAttribute
    {
        
            public override void OnActionExecuting(ActionExecutingContext filtercontext)
            {
                var value = filtercontext.HttpContext.Session.GetString("UserName");
                if (value == null)
                {
                    filtercontext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary {
                            {
                           "controller", "Login" },
                            { "action","Login" }
                            });
                }
            }
        }
    }


