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
    public class LoginBaseController : Controller
    {
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
            return Redirect(returnUrl);
        }


        #region Logined Session
        //Get Session Data
        public JsonResult GetSessionData()
        {
            return Json(SessionHelper.GetUserSession(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}