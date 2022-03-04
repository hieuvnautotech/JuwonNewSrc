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
    public class BomService : IBomService
    {
        private readonly IRepository repository;
        public BomService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<ResponseModel<int>> Active(int BomId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Bom_MofifyActive";
            var param = new DynamicParameters();
            param.Add("@bomId", BomId);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
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

        public async Task<ResponseModel<BomModel>> Create(BomModel model)
        {
            var returnData = new ResponseModel<BomModel>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Bom_Create";
            var param = new DynamicParameters();
            param.Add("@MoldId", model.MoldId);
            param.Add("@FileUpload", model.FileUpload);
            param.Add("@CreatedBy", createdBy);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
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
            catch (Exception e)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
            }
        }

        public async Task<ResponseModel<int>> Delete(int bomId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Bom_Delete";
            var param = new DynamicParameters();
            param.Add("@BomId", bomId);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
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

        public Task<ResponseModel<IList<BomModel>>> GetActive()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<BomModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<BomModel>> GetById(int id)
        {
            var returnData = new ResponseModel<BomModel>();
            string proc = "usp_Bom_GetBomById";
            var param = new DynamicParameters();
            param.Add("@BomId", id);
            var data = await repository.ExecuteReturnFirsOrDefault<BomModel>(proc, param);
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

        public Task<ResponseModel<BomModel>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<BomModel>> Modify(BomModel model)
        {
            var returnData = new ResponseModel<BomModel>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Bom_Modify";
            var param = new DynamicParameters();
            param.Add("@BomId", model.BomId);
            param.Add("@MoldId", model.MoldId);
            param.Add("@FileUpload", model.FileUpload);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
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
                        var data = await GetById(model.BomId);
                        returnData.ResponseMessage = Resource.SUCCESS_Modify;
                        returnData.Data = data.Data;
                        returnData.IsSuccess = true;
                        break;
                }
                return returnData;
            }
            catch (Exception e)
            {
                returnData.HttpResponseCode = 500;
                return returnData;
            }
        }

        public async Task<ResponseModel<IList<BomModel>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<BomModel>>();
            string proc = "usp_Bom_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<BomModel>(proc, param);
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

        public async Task<ResponseModel<IList<BomModel>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<BomModel>>();
            string proc = "usp_Bom_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<BomModel>(proc, param);
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