using Principal.Controllers;
using Principal.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Principal.Filters
{
    public class SessionVerify : ActionFilterAttribute
    {
        private User user;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                user = (User)HttpContext.Current.Session["user"];
                base.OnActionExecuting(filterContext);

                if (user == null)
                {
                    if (filterContext.Controller is SessionController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/SignIn/Session");
                    }
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/SignIn/Session");
            }
        }
    }
}