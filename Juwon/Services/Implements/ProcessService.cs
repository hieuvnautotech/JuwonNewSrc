using Dapper;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Juwon.Services.Interfaces;
using Juwon.Repository;
using Juwon.Models;
using Library.Common;
using Library.Helper;
using Library;

namespace Juwon.Services.Implements
{
    public class ProcessService : IProcessService
    {
        private readonly IRepository repository;
        private readonly string connectionString = DatabaseConnection.WRITE_CONNECTIONSTRING;

        public ProcessService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<int> BulkInsert(List<Process> insertList)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                //var dataTable = repository.ConvertListToDataTable(insertList);
                var effectedRows = await connection.BulkInsertAsync(insertList);
                if (effectedRows > 0)
                {
                    return 1;
                }
                return 0;
            }
        }

        public async Task<int> BulkMerge(List<Process> mergeList)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                //var dataTable = repository.ConvertListToDataTable(mergeList);
                var effectedRows = await connection.BulkMergeAsync(mergeList);
                if (effectedRows > 0)
                {
                    return 1;
                }
                return 0;
            }
        }

        public async Task<int> BulkUpdate(List<Process> updateList)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                //var dataTable = repository.ConvertListToDataTable(updateList);
                var effectedRows = await connection.BulkUpdateAsync(updateList);
                if (effectedRows > 0)
                {
                    return 1;
                }
                return 0;
            }
        }

        public async Task<ResponseModel<Process>> Create(Process model)
        {
            var returnData = new ResponseModel<Process>();

            //Process Code & Name cannot be blank
            if (string.IsNullOrWhiteSpace(model.ProcessCode) || string.IsNullOrWhiteSpace(model.ProcessName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_Process_Create";
            var param = new DynamicParameters();
            param.Add("@ProcessCode", model.ProcessCode);
            param.Add("@ProcessName", model.ProcessName);
            param.Add("@ProcessDescription", model.ProcessDescription);
            param.Add("@ProcessNote", model.ProcessNote);
            param.Add("@CreatedBy", model.CreatedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        break;
                    case -1:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedCode;
                        break;
                    case 1:
                        proc = "usp_Process_GetByCode";
                        param = new DynamicParameters();
                        param.Add("@ProcessCode", model.ProcessCode);
                        var data = await repository.ExecuteReturnFirsOrDefault<Process>(proc, param);
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.Data = data;
                        returnData.IsSuccess = true;
                        break;
                    default:
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
                throw;
            }
        }

        public async Task<ResponseModel<Process>> CreateTest(Process model)
        {
            var returnData = new ResponseModel<Process>();
            model.CreatedBy = SessionHelper.GetUserSession().ID;

            //Process Code & Name cannot be blank
            if (string.IsNullOrWhiteSpace(model.ProcessCode) || string.IsNullOrWhiteSpace(model.ProcessName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            var list = new List<Process>();
            model.Active = true;
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = SessionHelper.GetUserSession().ID;
            list.Add(model);

            try
            {
                //string sql = @"INSERT INTO dbo.Process(ProcessCode, ProcessName, ProcessDescription, ProcessNote, Active, CreatedDate, CreatedBy) VALUES(@ProcessCode, @ProcessName, @ProcessDescription, @ProcessNote, @Active, @CreatedDate, @CreatedBy) ";
                //var result = await repository.BulkInsertBySql<Process>(sql, list);
                var result = await BulkInsert(list);

                if (result > 0)
                {
                    string proc = "usp_Process_GetByCode";
                    var param = new DynamicParameters();
                    param.Add("@ProcessCode", model.ProcessCode);
                    var data = await repository.ExecuteReturnFirsOrDefault<Process>(proc, param);
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                    returnData.Data = data;
                    returnData.IsSuccess = true;
                }
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
                throw;
            }
        }

        public async Task<ResponseModel<int>> Delete(int processId)
        {

            var returnData = new ResponseModel<int>();
            var ModifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Process_DeleteByID";
            var param = new DynamicParameters();
            param.Add("@ProcessId", processId);
            param.Add("@ModifiedBy", ModifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        break;
                    case 1:
                        returnData.ResponseMessage = Resource.SUCCESS_Delete;
                        returnData.IsSuccess = true;
                        break;
                    default:
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
                throw;
            }
        }

        public async Task<ResponseModel<int>> Active(int processId)
        {
            var returnData = new ResponseModel<int>();
            var ModifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Process_Active";
            var param = new DynamicParameters();
            param.Add("@ProcessId", processId);
            param.Add("@ModifiedBy", ModifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        break;
                    case 1:
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.IsSuccess = true;
                        break;
                    default:
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
                throw;
            }
        }

        public async Task<ResponseModel<IList<Process>>> GetAll()
        {
            var returnData = new ResponseModel<IList<Process>>();
            string proc = "usp_Process_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<Process>(proc);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                    returnData.IsSuccess = true;
                }
                else
                {
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                    returnData.IsSuccess = false;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<IList<Process>>> GetActive()
        {
            var returnData = new ResponseModel<IList<Process>>();
            string proc = "usp_Process_GetAllActive";
            try
            {
                var result = await repository.ExecuteReturnList<Process>(proc);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                    returnData.IsSuccess = true;
                }
                else
                {
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                    returnData.IsSuccess = false;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<Process>> GetByCode(string processCode)
        {
            var returnData = new ResponseModel<Process>();
            string proc = "usp_Process_GetByCode";
            var param = new DynamicParameters();
            param.Add("@ProcessCode", processCode);
            var data = await repository.ExecuteReturnFirsOrDefault<Process>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;
                returnData.IsSuccess = true;
            }
            else
            {
                returnData.ResponseMessage = Resource.ERROR_NotFound;
                returnData.IsSuccess = false;
            }
            return returnData;
        }

        public async Task<ResponseModel<Process>> GetById(int processId)
        {
            var returnData = new ResponseModel<Process>();
            string proc = "usp_Process_GetById";
            var param = new DynamicParameters();
            param.Add("@ProcessId", processId);
            var data = await repository.ExecuteReturnFirsOrDefault<Process>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;
                returnData.IsSuccess = true;
            }
            else
            {
                returnData.ResponseMessage = Resource.ERROR_NotFound;
                returnData.IsSuccess = false;
            }
            return returnData;
        }

        public async Task<ResponseModel<Process>> GetByName(string processName)
        {
            var returnData = new ResponseModel<Process>();
            string proc = "usp_Process_GetByName";
            var param = new DynamicParameters();
            param.Add("@ProcessName", processName);
            var data = await repository.ExecuteReturnFirsOrDefault<Process>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;
                returnData.IsSuccess = true;
            }
            else
            {
                returnData.ResponseMessage = Resource.ERROR_NotFound;
                returnData.IsSuccess = false;
            }
            return returnData;
        }

        public async Task<ResponseModel<Process>> Modify(Process model)
        {
            var returnData = new ResponseModel<Process>();
            model.ModifiedBy = SessionHelper.GetUserSession().ID;

            //Process Code & Name cannot be blank
            if (string.IsNullOrWhiteSpace(model.ProcessCode) || string.IsNullOrWhiteSpace(model.ProcessName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_Process_Modify";
            var param = new DynamicParameters();
            param.Add("@ProcessId", model.ProcessId);
            param.Add("@ProcessName", model.ProcessName);
            param.Add("@ProcessDescription", model.ProcessDescription);
            param.Add("@ProcessNote", model.ProcessNote);
            param.Add("@ModifiedBy", model.ModifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        break;
                    case -2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        break;
                    case 1:
                        proc = "usp_Process_GetById";
                        param = new DynamicParameters();
                        param.Add("@ProcessId", model.ProcessId);
                        var data = await repository.ExecuteReturnFirsOrDefault<Process>(proc, param);
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.Data = data;
                        returnData.IsSuccess = true;
                        break;
                    default:
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
                throw;
            }
        }

        public async Task<ResponseModel<IList<Process>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Process>>();
            string proc = "usp_Process_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Process>(proc, param);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                }
                else
                {
                    returnData.Data = null;
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<IList<Process>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Process>>();
            string proc = "usp_Process_SearchAll";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Process>(proc, param);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                }
                else
                {
                    returnData.Data = null;
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

    }
}
