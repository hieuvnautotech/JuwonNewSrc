using Microsoft.AspNet.SignalR;

namespace Juwon.Hubs
{
    public class CusHub : Hub
    {
        public static void ShowStatus()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CusHub>();
            context.Clients.All.ShowStatus();
        }

        internal static void ControlUserLog()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CusHub>();
            context.Clients.All.ControlUserLog();
        }

        //public static void ChartDailyProduction()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CusHub>();
        //    context.Clients.All.ChartDailyProduction();
        //}

        //public static void DataDailyProduction()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CusHub>();
        //    context.Clients.All.DataDailyProduction();
        //}


        //public static void AllLines()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CusHub>();
        //    context.Clients.All.AllLines();
        //}

    }
}