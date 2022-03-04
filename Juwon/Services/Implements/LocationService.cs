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
    public class LocationService : ILocationService
    {
        private readonly IRepository repository;

        public LocationService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<LocationModel>> Create(LocationModel model)
        {
            var returnData = new ResponseModel<LocationModel>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Location_Create";
            var param = new DynamicParameters();
            param.Add("@LocationName", model.LocationName);
            param.Add("@LocationDescription", model.LocationDescription);
            param.Add("@LocationCategoryId", model.LocationCategoryId);
            param.Add("@AreaId", model.AreaId);
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

        public async Task<ResponseModel<int>> Delete(int locationId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Location_Delete";
            var param = new DynamicParameters();
            param.Add("@LocationId", locationId);
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

        public async Task<ResponseModel<int>> Active(int locationId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Location_Active";
            var param = new DynamicParameters();
            param.Add("@LocationId", locationId);
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

        public async Task<ResponseModel<IList<LocationModel>>> GetActive()
        {
            var returnData = new ResponseModel<IList<LocationModel>>();
            string proc = "usp_Location_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<LocationModel>(proc);
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

        public async Task<ResponseModel<IList<LocationModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<LocationModel>>();
            string proc = "usp_Location_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<LocationModel>(proc);
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

        public async Task<ResponseModel<LocationModel>> GetById(int locationId)
        {
            var returnData = new ResponseModel<LocationModel>();
            string proc = "usp_Location_GetById";
            var param = new DynamicParameters();
            param.Add("@LocationId", locationId);
            var data = await repository.ExecuteReturnFirsOrDefault<LocationModel>(proc, param);
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

        public async Task<ResponseModel<LocationModel>> GetByName(string locationName)
        {
            var returnData = new ResponseModel<LocationModel>();
            string proc = "usp_Location_GetByName";
            var param = new DynamicParameters();
            param.Add("@LocationName", locationName);
            var data = await repository.ExecuteReturnFirsOrDefault<LocationModel>(proc, param);
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

        public async Task<ResponseModel<LocationModel>> Modify(LocationModel model)
        {
            var returnData = new ResponseModel<LocationModel>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Location_Modify";
            var param = new DynamicParameters();
            param.Add("@LocationId", model.LocationId);
            param.Add("@LocationName", model.LocationName);
            param.Add("@LocationDescription", model.LocationDescription);
            param.Add("@LocationCategoryId", model.LocationCategoryId);
            param.Add("@AreaId", model.AreaId);
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
                        var data = await GetById(model.LocationId);
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

        public async Task<ResponseModel<IList<LocationModel>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<LocationModel>>();
            string proc = "usp_Location_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<LocationModel>(proc, param);
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

        public async Task<ResponseModel<IList<LocationModel>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<LocationModel>>();
            string proc = "usp_Location_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<LocationModel>(proc, param);
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