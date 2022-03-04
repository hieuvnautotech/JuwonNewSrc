using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Juwon.Repository
{
    public static class DatabaseConnection
    {

        /*
         <add name="_DbContext" connectionString="data source=118.69.61.10;initial catalog=Juwon;persist security info=True;user id=sa;password=Onconc848682@#!;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
         */

        /**
         * Connection String Set up
         */

        //// AUTONSI-JUWON NAT
        //public static readonly string CONNECTIONSTRING = @"data source=118.69.61.10;initial catalog=Juwon;persist security info=True;user id=sa;password=Onconc848682@#!;MultipleActiveResultSets=True;";
        //public static readonly string WRITE_CONNECTIONSTRING = @"data source=118.69.61.10;initial catalog=Juwon;persist security info=True;user id=sa;password=Onconc848682@#!;MultipleActiveResultSets=True;";
        //public static readonly string READ_CONNECTIONSTRING = @"data source=118.69.61.10;initial catalog=Juwon;persist security info=True;user id=sa;password=Onconc848682@#!;MultipleActiveResultSets=True;";

        public static readonly string CONNECTIONSTRING = @"data source=.;initial catalog=Juwon;persist security info=True;user id=sa;password=root;MultipleActiveResultSets=True;";
        public static readonly string WRITE_CONNECTIONSTRING = @"data source=.;initial catalog=Juwon;persist security info=True;user id=sa;password=root;MultipleActiveResultSets=True;";
        public static readonly string READ_CONNECTIONSTRING = @"data source=.;initial catalog=Juwon;persist security info=True;user id=sa;password=root;MultipleActiveResultSets=True;";

        //Write connectionString
        //public static readonly string WRITE_CONNECTIONSTRING = @"data source=192.168.5.11;initial catalog=Juwon;persist security info=True;user id=sa;password=@admin2k21;MultipleActiveResultSets=True;";
        //public static readonly string WRITE_CONNECTIONSTRING = @"data source=118.69.61.10;initial catalog=Juwon;persist security info=True;user id=sa;password=Onconc848682@#!;MultipleActiveResultSets=True;";

        //Read connectionString
        //public static readonly string READ_CONNECTIONSTRING = @"data source=192.168.5.73;initial catalog=RepJuwon;persist security info=True;user id=sa;password=root;MultipleActiveResultSets=True;";
        //public static readonly string READ_CONNECTIONSTRING = @"data source=118.69.61.10;initial catalog=Juwon;persist security info=True;user id=sa;password=Onconc848682@#!;MultipleActiveResultSets=True;";

        public static async Task<T> ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
            {
                connection.Open();
                var result = await connection.ExecuteScalarAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
