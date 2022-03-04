using Dapper;
using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using Library;
using Library.Common;
using Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Juwon.Services.Implements
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository repository;

        public DepartmentService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<Department>> Create(Department model)
        {

            var returnData = new ResponseModel<Department>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Department_Create";
            var param = new DynamicParameters();
            param.Add("@DepartmentName", model.DepartmentName);
            param.Add("@CreatedBy", createdBy);
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
                    case 0:
                        break;
                    default:
                        model.DepartmentId = result;
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

        public async Task<ResponseModel<int>> Delete(int areaCategoryId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Department_Delete";
            var param = new DynamicParameters();
            param.Add("@DepartmentId", areaCategoryId);
            param.Add("@ModifiedDate", DateTime.Now);
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

       

        public Task<ResponseModel<IList<Department>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Department>> GetByName(string partName)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Department>> Modify(Department model)
        {
            var returnData = new ResponseModel<Department>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Department_Modify";
            var param = new DynamicParameters();
            param.Add("@DepartmentId", model.DepartmentId);
            param.Add("@DepartmentName", model.DepartmentName);
            param.Add("@ModifiedDate", DateTime.Now);
            param.Add("@ModifiedBy", modifiedBy);
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
                    case -1:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedCode;
                        break;
                    case 0:
                        break;
                    default:
                        var data = await GetById(model.DepartmentId);
                        returnData.ResponseMessage = Resource.SUCCESS_Modify;
                        returnData.Data = data.Data;
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

        public async Task<ResponseModel<Department>> GetById(int partId)
        {
            var returnData = new ResponseModel<Department>();
            string proc = "usp_Department_GetById";
            var param = new DynamicParameters();
            param.Add("@DepartmentId", partId);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<Department>(proc, param);
                if (result != null)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                    returnData.IsSuccess = true;
                }
                else
                {
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }

                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<IList<Department>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Department>>();
            string proc = $"usp_Department_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Department>(proc, param);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                }
                else
                {
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
                throw;
            }
        }

        public async Task<ResponseModel<IList<Department>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Department>>();
            string proc = $"usp_Department_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Department>(proc, param);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                }
                else
                {
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
                throw;
            }
        }

        public async Task<ResponseModel<int>> Active(int departmentId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Department_Active";
            var param = new DynamicParameters();
            param.Add("@DepartmentId", departmentId);
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

        public Task<ResponseModel<IList<Department>>> GetActive()
        {
            throw new NotImplementedException();
        }
    }
}