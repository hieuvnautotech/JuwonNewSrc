using Library;
using Library.Common;
using Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Juwon.Controllers.Base
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var session = (UserModel)Session[CommonConstants.USER_SESSION];
            var session = SessionHelper.GetUserSession();
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index"/*, Area = "Admin"*/ }));
            }

            else
            {
                string sKey = (string)HttpContext.Cache[session.UserName];
                string sUser = session.SessionId;
                if (sKey != sUser)
                {

                    //NotificationComponents nc = new NotificationComponents();
                    //nc.AppEnd();

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Logout"/*, Area = "Admin"*/ }));
                }
            }


            //filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            //filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            //filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            //filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnActionExecuting(filterContext);
        }

        //initializing culture on controller initialization
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (SessionHelper.GetCultureSession() != null)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(SessionHelper.GetCultureSession());
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(SessionHelper.GetCultureSession());
            }
            else
            {
                SessionHelper.SetCultureSession("en");
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
        }

        //change culture
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ddlCulture);
            SessionHelper.SetCultureSession(ddlCulture);
            var user = SessionHelper.GetUserSession();
            foreach (var item in user.Menus)
            {
                item.MultiLang = CommonMethods.GetResourceTitle<Resource>(string.Concat("MML_", item.Name));
            }
            SessionHelper.SetUserSession(user);
            return Redirect(returnUrl);
        }

        public JsonResult GetResourceValue(string input)
        {
            string output;
            input = input ?? "";
            if (CommonMethods.GetResourceTitle<Resource>(input) == null)
            {
                output = "notExisted";
            }
            else
            {
                output = CommonMethods.GetResourceTitle<Resource>(input);
            }
            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}