
using Library.Common;
using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Library;
using Library.Helper;
using System.Data.SqlClient;
using RepoDb;

namespace Juwon.Services.Implements
{
    public class StaffHieuService : IStaffHieuService
    {
        private readonly IRepository repository;
        private readonly string connectionString = DatabaseConnection.CONNECTIONSTRING;
        public StaffHieuService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<int> BulkInsert(List<Staff> insertList)
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

        public async Task<ResponseModel<Staff>> Create(Staff model)
        {

            var returnData = new ResponseModel<Staff>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Supplier_Create";
            var param = new DynamicParameters();
            param.Add("@StaffName", model.StaffName);
            param.Add("@StaffCode", model.StaffCode);
            param.Add("@Departmentid", model.DepartmentId);          
            param.Add("@CreatedBy", createdBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        break;
                    case 0:
                        break;
                    default:
                        model.StaffId = result;
                        model.CreatedDate = DateTime.Now;
                        returnData.ResponseMessage = Resource.SUCCESS_Create;
                        returnData.Data = model;
                        returnData.IsSuccess = true;
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
            }
        }

        public async Task<ResponseModel<int>> Delete(int staffId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Supplier_Delete";
            var param = new DynamicParameters();
            param.Add("@staffId", staffId);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        break;
                    case 0:
                        break;
                    default:
                        returnData.ResponseMessage = Resource.SUCCESS_Delete;
                        returnData.Data = result;
                        returnData.IsSuccess = true;
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
            }
        }

        public Task<ResponseModel<IList<Staff>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<Staff>>> GetActive()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Staff>> GetById(int processId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Staff>> GetByName(string processName)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Staff>> Modify(Staff model)
        {
            var returnData = new ResponseModel<Staff>();
            model.ModifiedBy = SessionHelper.GetUserSession().ID;
            //Name cannot be blank
            if (string.IsNullOrWhiteSpace(model.StaffName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_Supplier_Modify";
            var param = new DynamicParameters();
            param.Add("@StaffId", model.StaffId);
            param.Add("@StaffName", model.StaffName);
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
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.Data = model;
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

        public async Task<ResponseModel<IList<Staff>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Staff>>();
            string proc = "usp_Supplier_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Staff>(proc, param);
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

        public async Task<ResponseModel<IList<Staff>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Staff>>();
            string proc = "usp_Supplier_SearchAll";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Staff>(proc, param);
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

        public async Task<ResponseModel<int>> Active(int staffId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Supplier_Active";
            var param = new DynamicParameters();
            param.Add("@staffId", staffId);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        break;
                    case 0:
                        break;
                    default:
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.Data = result;
                        returnData.IsSuccess = true;
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
            }
        }
    }
}
