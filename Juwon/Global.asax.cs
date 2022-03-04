using Juwon.Hubs;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Juwon.Repository;
using Juwon.App_Start;

namespace Juwon
{
    public class MvcApplication : HttpApplication
    {
        //string con = ConfigurationManager.ConnectionStrings["_DbContext"].ConnectionString;
        private readonly string con = DatabaseConnection.CONNECTIONSTRING;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configure(WebApiConfig.Register);

            //Unity.Mvc5 Dependency Injection
            UnityConfig.RegisterComponents();

            //RepoDb Initialize
            RepoDb.SqlServerBootstrap.Initialize();

            //Start SqlDependency with application initialization (SignalR)
            //SqlDependency.Start(con);
        }

        protected void Application_End()
        {
            //Stop SqlDependency(SignalR)
            SqlDependency.Stop(con);
            NotificationComponents nc = new NotificationComponents();
            nc.AppEnd();
        }

        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    //var currentTime = DateTime.Now;
        //    //NotificationComponents nc = new NotificationComponents();

        //    //HttpContext.Current.Session["LastLog"] = currentTime;
        //    //nc.RegisterNotification(currentTime);
        //    if (Session[CommonConstants.USER_SESSION] != null) // e.g. this is after an initial logon
        //    {
        //        var obj = Session[CommonConstants.USER_SESSION] as UserDTO;
        //        // Accessing the Cache Item extends the Sliding Expiration automatically
        //        string sKey = obj.ID.ToString() + Session.SessionID.ToString();
        //        HttpContext.Current.Cache["sKey"] = sKey;
        //    }
        //    else
        //    {

        //    }
        //}

        protected void Session_End()
        {
            ////Stop SqlDependency (SignalR)
            //SqlDependency.Stop(con);
            NotificationComponents nc = new NotificationComponents();
            nc.AppEnd();
        }

        //protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        //{
        //    // Let's write a message to show this got fired---
        //    //Response.Write("SessionID: " + Session.SessionID.ToString() + "User key: " + (string)Session[CommonConstants.USER_SESSION]);
        //    if (Session[CommonConstants.USER_SESSION] != null) // e.g. this is after an initial logon
        //    {
        //        var obj = Session[CommonConstants.USER_SESSION] as UserDTO;
        //        // Accessing the Cache Item extends the Sliding Expiration automatically
        //        string sKey = obj.ID.ToString() + Session.SessionID.ToString();
        //        HttpContext.Current.Cache["sKey"] = sKey;
        //    }
        //}
    }
}
