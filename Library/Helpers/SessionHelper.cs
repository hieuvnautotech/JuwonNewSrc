using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Library.Helper
{
    public static class SessionHelper
    {

        //// Get, Set User Session
        public static void SetUserSession(UserModel model)
        {
            HttpContext.Current.Session[CommonConstants.USER_SESSION] = model;
        }
        public static UserModel GetUserSession()
        {
            var session = HttpContext.Current.Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return null;
            }
            else
            {
                return session as UserModel;
            }
        }

        //// Get, Set Culture Session
        public static void SetCultureSession(string ddlCulture)
        {
            HttpContext.Current.Session[CommonConstants.CURRENT_CULTURE] = ddlCulture;
        }
        public static string GetCultureSession()
        {
            var session = HttpContext.Current.Session[CommonConstants.CURRENT_CULTURE];
            if (session == null)
            {
                return null;
            }
            else
            {
                return session as string;
            }
        }
    }
}