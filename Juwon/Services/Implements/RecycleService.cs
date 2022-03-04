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
    public class RecycleService : IRecycleService
    {
        private readonly IRepository repository;
        private readonly string connectionString = DatabaseConnection.CONNECTIONSTRING;
        public RecycleService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<ResponseModel<int>> Active(int recycleId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Recycle_Active";
            var param = new DynamicParameters();
            param.Add("@recycleId", recycleId);
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

        public async Task<ResponseModel<Recycle>> Create(Recycle model)
        {
            var returnData = new ResponseModel<Recycle>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Recycle_Create";
            var param = new DynamicParameters();
            param.Add("@RecycleName", model.RecycleName);
            param.Add("@RecycleDescription", model.RecycleDescription);
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
                        model.RecycleId = result;
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

        public async Task<ResponseModel<int>> Delete(int recycleId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Recycle_Delete";
            var param = new DynamicParameters();
            param.Add("@recycleId", recycleId);
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

        public Task<ResponseModel<IList<Recycle>>> GetActive()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<Recycle>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Recycle>> GetById(int RecycleId)
        {
            var returnData = new ResponseModel<Recycle>();
            string proc = "usp_Recycle_GetById";
            var param = new DynamicParameters();
            param.Add("@RecycleId", RecycleId);
            var data = await repository.ExecuteReturnFirsOrDefault<Recycle>(proc, param);
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

        public Task<ResponseModel<Recycle>> GetByName(string qcMasterName)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Recycle>> Modify(Recycle model)
        {
            var returnData = new ResponseModel<Recycle>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Recycle_Modify";
            var param = new DynamicParameters();
            param.Add("@RecycleId", model.RecycleId);
            param.Add("@RecycleName", model.RecycleName);
            param.Add("@RecycleDescription", model.RecycleDescription);
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
                        var data = await GetById(model.RecycleId);
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

        public async Task<ResponseModel<IList<Recycle>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Recycle>>();
            string proc = "usp_Recycle_SearchActive";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Recycle>(proc, param);
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

        public async Task<ResponseModel<IList<Recycle>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Recycle>>();
            string proc = "usp_Recycle_SearchAll";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Recycle>(proc, param);
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