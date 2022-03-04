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
    public class LocationCategoryService : ILocationCategoryService
    {
        private readonly IRepository repository;

        public LocationCategoryService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<int>> Active(int locationCategoryId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_LocationCategory_Active";
            var param = new DynamicParameters();
            param.Add("@LocationCategoryId", locationCategoryId);
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

        public async Task<ResponseModel<LocationCategory>> Create(LocationCategory model)
        {
            var returnData = new ResponseModel<LocationCategory>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_LocationCategory_Create";
            var param = new DynamicParameters();
            param.Add("@LocationCategoryName", model.LocationCategoryName);
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
                        model.LocationCategoryId = result;
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

        public async Task<ResponseModel<int>> Delete(int locationCategoryId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_LocationCategory_Delete";
            var param = new DynamicParameters();
            param.Add("@LocationCategoryId", locationCategoryId);
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

        public async Task<ResponseModel<IList<LocationCategory>>> GetActive()
        {
            var returnData = new ResponseModel<IList<LocationCategory>>();
            string proc = "usp_LocationCategory_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<LocationCategory>(proc);
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

        public async Task<ResponseModel<IList<LocationCategory>>> GetAll()
        {
            var returnData = new ResponseModel<IList<LocationCategory>>();
            string proc = "usp_LocationCategory_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<LocationCategory>(proc);
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

        public async Task<ResponseModel<LocationCategory>> GetById(int locationCategoryId)
        {
            var returnData = new ResponseModel<LocationCategory>();
            string proc = "usp_LocationCategory_GetById";
            var param = new DynamicParameters();
            param.Add("@LocationCategoryId", locationCategoryId);
            var data = await repository.ExecuteReturnFirsOrDefault<LocationCategory>(proc, param);
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

        public async Task<ResponseModel<LocationCategory>> GetByName(string locationCategoryName)
        {
            var returnData = new ResponseModel<LocationCategory>();
            string proc = "usp_LocationCategory_GetByName";
            var param = new DynamicParameters();
            param.Add("@LocationCategoryName", locationCategoryName);
            var data = await repository.ExecuteReturnFirsOrDefault<LocationCategory>(proc, param);
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

        public async Task<ResponseModel<LocationCategory>> Modify(LocationCategory model)
        {
            var returnData = new ResponseModel<LocationCategory>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_LocationCategory_Modify";
            var param = new DynamicParameters();
            param.Add("@LocationCategoryId", model.LocationCategoryId);
            param.Add("@LocationCategoryName", model.LocationCategoryName);
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
                        var data = await GetById(model.LocationCategoryId);
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

        public async Task<ResponseModel<IList<LocationCategory>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<LocationCategory>>();
            string proc = "usp_LocationCategory_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<LocationCategory>(proc, param);
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

        public async Task<ResponseModel<IList<LocationCategory>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<LocationCategory>>();
            string proc = "usp_LocationCategory_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<LocationCategory>(proc, param);
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