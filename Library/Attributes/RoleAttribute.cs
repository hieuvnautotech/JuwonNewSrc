using Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;


namespace Library.Attributes
{
    public class RoleAttribute : AuthorizeAttribute
    {
        public RoleAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized)
            //{
            //    return false;
            //}
            //else
            //{
            //}

            var flag = false;
            var session = SessionHelper.GetUserSession();
            if (session == null)
            {
                //filterContext.Result = new RedirectToRouteResult(new
                //RouteValueDictionary(new { controller = "Unauthorize", action = "Index" }));
                return flag;
            }
            else
            {
                var roles = session.Roles;

                var listOfRoles = new List<string>(Roles.Split(','));

                if (listOfRoles.Any(roles.Contains))
                {
                    flag = true;
                }
            }
            return flag;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (System.Web.HttpContext.Current.Request.HttpMethod == "GET" && System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString() == "Index")
            {
                //filterContext.Result = new ViewResult
                //{
                //    ViewName = "~/Views/401/Index.cshtml"
                //};

                // filterContext.Result = new RedirectToRouteResult(new
                //RouteValueDictionary(new { controller = "AccessDenied" }));

                filterContext.Result = new RedirectResult("~/AccessDenied");
            }
            else
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { flag = false, message = Resource.ERROR_AccountUnAuthorized },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    ContentType = "application/json",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}
