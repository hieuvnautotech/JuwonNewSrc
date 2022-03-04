using Dapper;
//using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Juwon.Repository
{
    public class RepositoryImplement : IRepository
    {
        #region CONNECTIONS STRING FOR PUBLISH PURPOSES
        /**
         * SET CONNECTION STRING WHEN PUBLISH
         */
        //private static readonly string connectionString = DatabaseConnection.CONNECTIONSTRING;
        private static readonly string write_connectionString = DatabaseConnection.WRITE_CONNECTIONSTRING;
        private static readonly string read_connectionString = DatabaseConnection.READ_CONNECTIONSTRING;
        #endregion

        #region BE USED WHEN EXECUTE A STORED PROCEDURE

        #region Read Database
        public async Task<T> ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(read_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await connection.ExecuteScalarAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<T> ExecuteReturnFirsOrDefault<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(read_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IList<T>> ExecuteReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(read_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                try
                {
                    var result = await connection.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task<DataTable> ExecuteReturnDataTable(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(read_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DataTable returnTable = new DataTable();
                var result = await connection.ExecuteReaderAsync(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

                if (result.HasRows)
                {
                    returnTable.Load(result);
                }
                return returnTable;
            }
        }
        #endregion

        #region Write Database
        public async Task<T> Write_ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(write_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await connection.ExecuteScalarAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<T> Write_ExecuteReturnFirsOrDefault<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(write_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IList<T>> Write_ExecuteReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(write_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                try
                {
                    var result = await connection.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task<DataTable> Write_ExecuteReturnDataTable(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(write_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DataTable returnTable = new DataTable();
                var result = await connection.ExecuteReaderAsync(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

                if (result.HasRows)
                {
                    returnTable.Load(result);
                }
                return returnTable;
            }
        }
        #endregion

        #endregion

        #region BE USED WHEN EXECUTE A QUERY

        #region Read Database
        public async Task<T> GetReturnScalar<T>(string sql, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(read_connectionString))
            {
                connection.Open();
                var result = await connection.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text);
                return result;
            }
        }

        public async Task<T> GetReturnFirstOrDefault<T>(string sql, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(read_connectionString))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.Text);
                return result;
            }
        }

        public async Task<IList<T>> GetReturnList<T>(string sql, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(read_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(sql, param, commandType: CommandType.Text);
                return result.ToList();
            }
        }

        public async Task<DataTable> GetReturnDataTable(string sql, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(read_connectionString))
            {
                connection.Open();
                DataTable returnTable = new DataTable();
                var result = await connection.ExecuteReaderAsync(sql, param, commandType: CommandType.Text);

                if (result.HasRows)
                {
                    returnTable.Load(result);
                }
                return returnTable;
            }
        }
        #endregion

        #region Write Database
        public async Task<T> Write_GetReturnScalar<T>(string sql, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(write_connectionString))
            {
                connection.Open();
                var result = await connection.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text);
                return result;
            }
        }

        public async Task<T> Write_GetReturnFirstOrDefault<T>(string sql, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(write_connectionString))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.Text);
                return result;
            }
        }

        public async Task<IList<T>> Write_GetReturnList<T>(string sql, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(write_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(sql, param, commandType: CommandType.Text);
                return result.ToList();
            }
        }

        public async Task<DataTable> Write_GetReturnDataTable(string sql, DynamicParameters param = null)
        {
            using (SqlConnection connection = new SqlConnection(write_connectionString))
            {
                connection.Open();
                DataTable returnTable = new DataTable();
                var result = await connection.ExecuteReaderAsync(sql, param, commandType: CommandType.Text);

                if (result.HasRows)
                {
                    returnTable.Load(result);
                }
                return returnTable;
            }
        }
        #endregion

        #endregion

        #region BULK OPERATIONS
        public async Task<int> BulkInsertBySql<T>(string sql, List<T> obj)
        {
            //using (TransactionScope scope = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(write_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var trans = connection.BeginTransaction();
                try
                {
                    await connection.ExecuteAsync(sql, obj, transaction: trans);
                    trans.Commit();
                    //scope.Complete();
                    return 1;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return 0;
                    throw;
                }
            }
        }
        #endregion
    }
}
