using System;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;

namespace HelloWorld
{
    public class AuthorizeIPAddressAttribute : ActionFilterAttribute
    {
        // Called by the ASP.NET MVC framework BEFORE the action method executes.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRequestIP = filterContext.HttpContext.Request.UserHostAddress;

            if (currentRequestIP == "::1")
            {
                //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                throw new Exception();
            }

            base.OnActionExecuting(filterContext);
        }      
    }
}