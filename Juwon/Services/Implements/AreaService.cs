using Dapper;
using Juwon.Models;
using Juwon.Models.DTOs;
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
    public class AreaService : IAreaService
    {
        private readonly IRepository repository;

        public AreaService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<AreaModel>> Create(AreaModel model)
        {
            var returnData = new ResponseModel<AreaModel>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Area_Create";
            var param = new DynamicParameters();
            param.Add("@AreaName", model.AreaName);
            param.Add("@AreaDescription", model.AreaDescription);
            param.Add("@AreaCategoryId", model.AreaCategoryId);
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
                        var data = await GetById(result);
                        returnData.ResponseMessage = Resource.SUCCESS_Create;
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

        public async Task<ResponseModel<int>> Delete(int areaId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Area_Delete";
            var param = new DynamicParameters();
            param.Add("@AreaId", areaId);
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

        public async Task<ResponseModel<int>> Active(int areaId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Area_Active";
            var param = new DynamicParameters();
            param.Add("@AreaId", areaId);
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

        public async Task<ResponseModel<IList<AreaModel>>> GetActive()
        {
            var returnData = new ResponseModel<IList<AreaModel>>();
            string proc = "usp_Area_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<AreaModel>(proc);
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

        public async Task<ResponseModel<IList<AreaModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<AreaModel>>();
            string proc = "usp_Area_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<AreaModel>(proc);
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

        public async Task<ResponseModel<AreaModel>> GetById(int id)
        {
            var returnData = new ResponseModel<AreaModel>();
            string proc = "usp_Area_GetAreaById";
            var param = new DynamicParameters();
            param.Add("@AreaId", id);
            var data = await repository.ExecuteReturnFirsOrDefault<AreaModel>(proc, param);
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

        public async Task<ResponseModel<AreaModel>> GetByName(string name)
        {
            var returnData = new ResponseModel<AreaModel>();
            string proc = "usp_Area_GetAreaByName";
            var param = new DynamicParameters();
            param.Add("@AreaName", name);
            var data = await repository.ExecuteReturnFirsOrDefault<AreaModel>(proc, param);
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

        public async Task<ResponseModel<AreaModel>> Modify(AreaModel model)
        {
            var returnData = new ResponseModel<AreaModel>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Area_Modify";
            var param = new DynamicParameters();
            param.Add("@AreaId", model.AreaId);
            param.Add("@AreaName", model.AreaName);
            param.Add("@AreaDescription", model.AreaDescription);
            param.Add("@AreaCategoryId", model.AreaCategoryId);
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
                    case 0:
                        break;
                    default:
                        var data = await GetById(model.AreaId);
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

        public async Task<ResponseModel<IList<AreaModel>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<AreaModel>>();
            string proc = "usp_Area_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<AreaModel>(proc,param);
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

        public async Task<ResponseModel<IList<AreaModel>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<AreaModel>>();
            string proc = "usp_Area_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<AreaModel>(proc,param);
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