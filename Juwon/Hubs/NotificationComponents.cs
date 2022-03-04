using Dapper;
using Library.Common;
using Library.Helper;
using Juwon.Repository;
using Microsoft.AspNet.SignalR;
using System;
using System.Data.SqlClient;

namespace Juwon.Hubs
{
    public class NotificationComponents
    {

        public void RegisterNotification(DateTime currentTime)
        {
            string connectionString = DatabaseConnection.CONNECTIONSTRING;
            string sqlCommand = $"SELECT [Id], [UserInfoId], [Active], [DateTimeLog] FROM [dbo].[UserLog] WHERE [DateTimeLog] < @DatimeLog";
            using (var connection = new SqlConnection(DatabaseConnection.CONNECTIONSTRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Notification = null;
                    command.Parameters.AddWithValue("@DatimeLog", currentTime);
                    SqlDependency dependency = new SqlDependency(command);
                    //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    dependency.OnChange += dependency_OnChange;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                    }
                    
                    
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= dependency_OnChange;
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<CusHub>();
                notificationHub.Clients.All.notify("Log");

                RegisterNotification(DateTime.Now);
            }
        }

        public async void AppEnd()
        {
            UserModel userDTO = SessionHelper.GetUserSession();
            if (userDTO != null)
            {
                int userId = userDTO.ID;
                string ipAddress = userDTO.IpAddress;
                string sessionId = userDTO.SessionId;
                string proc = "usp_UserInfo_Logout";
                var param = new DynamicParameters();
                param.Add("@UserInfoId", userId);
                param.Add("@IpAddress", ipAddress);
                param.Add("@SessionId", sessionId);
                await DatabaseConnection.ExecuteReturnScalar<int>(proc, param); 
            }
        }

    }
}