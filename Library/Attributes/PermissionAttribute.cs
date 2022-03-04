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
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(params string[] permissions) : base()
        {
            Roles = string.Join(",", permissions);
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

            var session = SessionHelper.GetUserSession();
            var permissions = session.Permissions;

            var listOfPermissions = new List<string>(Roles.Split(','));

            if (listOfPermissions.All(permissions.Contains))
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new ViewResult
            //{
            //    ViewName = "~/Views/401/Index.cshtml"
            //};
            //filterContext.Result = new RedirectToRouteResult(new
            //RouteValueDictionary(new { controller = "Unauthorize", action = "Index" }));
            //filterContext.Result = new HttpUnauthorizedResult();

            if (System.Web.HttpContext.Current.Request.HttpMethod == "GET")
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/401/Index.cshtml"
                };
            }
            else
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { flag = false, Resource.ERROR_AccountUnAuthorized },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    ContentType = "application/json",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}
