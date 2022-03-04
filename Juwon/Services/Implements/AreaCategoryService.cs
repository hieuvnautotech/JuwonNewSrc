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
    public class AreaCategoryService : IAreaCategoryService
    {
        private readonly IRepository repository;

        public AreaCategoryService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<AreaCategory>> Create(AreaCategory model)
        {

            var returnData = new ResponseModel<AreaCategory>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_AreaCategory_Create";
            var param = new DynamicParameters();
            param.Add("@AreaCategoryName", model.AreaCategoryName);
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
                        model.AreaCategoryId = result;
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
            string proc = $"usp_AreaCategory_Delete";
            var param = new DynamicParameters();
            param.Add("@AreaCategoryId", areaCategoryId);
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

        public async Task<ResponseModel<IList<AreaCategory>>> GetActive()
        {
            var returnData = new ResponseModel<IList<AreaCategory>>();
            string proc = $"usp_AreaCategory_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<AreaCategory>(proc);
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

        public Task<ResponseModel<IList<AreaCategory>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<AreaCategory>> GetByName(string partName)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<AreaCategory>> Modify(AreaCategory model)
        {
            var returnData = new ResponseModel<AreaCategory>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_AreaCategory_Modify";
            var param = new DynamicParameters();
            param.Add("@AreaCategoryId", model.AreaCategoryId);
            param.Add("@AreaCategoryName", model.AreaCategoryName);
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
                        var data = await GetById(model.AreaCategoryId);
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

        public async Task<ResponseModel<AreaCategory>> GetById(int partId)
        {
            var returnData = new ResponseModel<AreaCategory>();
            string proc = "usp_AreaCategory_GetById";
            var param = new DynamicParameters();
            param.Add("@AreaCategoryId", partId);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<AreaCategory>(proc, param);
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

        public async Task<ResponseModel<IList<AreaCategory>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<AreaCategory>>();
            string proc = $"usp_AreaCategory_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<AreaCategory>(proc, param);
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

        public async Task<ResponseModel<IList<AreaCategory>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<AreaCategory>>();
            string proc = $"usp_AreaCategory_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<AreaCategory>(proc, param);
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

        public async Task<ResponseModel<int>> Active(int areaCategoryId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_AreaCategory_Active";
            var param = new DynamicParameters();
            param.Add("@AreaCategoryId", areaCategoryId);
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