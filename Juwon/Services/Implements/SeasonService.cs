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
    public class SeasonService : ISeasonService
    {
        private readonly IRepository repository;

        public SeasonService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<ResponseModel<Season>> Create(Season model)
        {
            var returnData = new ResponseModel<Season>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Season_Create";
            var param = new DynamicParameters();
            param.Add("@SeasonCode", model.SeasonCode);
            param.Add("@SeasonName", model.SeasonName);
            param.Add("@SeasonDescription", model.SeasonDescription);
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
                        model.SeasonId = result;
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

        public async Task<ResponseModel<int>> Delete(int seasonId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Season_Delete";
            var param = new DynamicParameters();
            param.Add("@SeasonId", seasonId);
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

        public async Task<ResponseModel<int>> Active(int seasonId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Season_Active";
            var param = new DynamicParameters();
            param.Add("@SeasonId", seasonId);
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

        public Task<ResponseModel<IList<Season>>> GetActive()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<Season>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Season>> GetById(int SeasonId)
        {
            var returnData = new ResponseModel<Season>();
            string proc = "usp_Season_GetById";
            var param = new DynamicParameters();
            param.Add("@SeasonId", SeasonId);
            var data = await repository.ExecuteReturnFirsOrDefault<Season>(proc, param);
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

        public Task<ResponseModel<Season>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Season>> Modify(Season model)
        {
            var returnData = new ResponseModel<Season>();
            model.ModifiedBy = SessionHelper.GetUserSession().ID;
            //Code & name cannot be blank
            if (string.IsNullOrWhiteSpace(model.SeasonCode))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }
            if (string.IsNullOrWhiteSpace(model.SeasonName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }
            string proc = "usp_Season_Modify";
            var param = new DynamicParameters();
            param.Add("@SeasonId", model.SeasonId);
            param.Add("@SeasonCode", model.SeasonCode);
            param.Add("@SeasonName", model.SeasonName);
            param.Add("@SeasonDescription", model.SeasonDescription);
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
                        var data = await GetById(model.SeasonId);
                        returnData.ResponseMessage = Resource.SUCCESS_Modify;
                        returnData.Data = data.Data;
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

        public async Task<ResponseModel<IList<Season>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Season>>();
            string proc = "usp_Season_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Season>(proc, param);
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

        public async Task<ResponseModel<IList<Season>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Season>>();
            string proc = "usp_Season_SearchAll";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Season>(proc, param);
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