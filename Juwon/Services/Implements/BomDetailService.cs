using Dapper;
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
    public class BomDetailService : IBomDetailService
    {
        private readonly IRepository repository;

        public BomDetailService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<BomDetailModel>> Create(BomDetailModel model)
        {
            var returnData = new ResponseModel<BomDetailModel>();
            model.CreatedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_BomDetail_Create";
            var param = new DynamicParameters();
            param.Add("@BomId", model.BomId);
            param.Add("@PartId", model.PartId);
            param.Add("@MaterialId", model.MaterialId);
            param.Add("@Weight", model.Weight);
            param.Add("@Amount", model.Amount);
            param.Add("@Image", model.Image);
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

        public async Task<ResponseModel<int>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<BomDetailModel>>> GetActive()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<BomDetailModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<IList<BomDetailModel>>> GetActiveBomDetailByBomId(int bomId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<IList<BomDetailModel>>> GetAllBomDetailByBomId(int bomId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<BomDetailModel>> GetById(int id)
        {
            var returnData = new ResponseModel<BomDetailModel>();
            string proc = "usp_BomDetail_GetById";
            var param = new DynamicParameters();
            param.Add("@BomDetailId", id);
            var data = await repository.ExecuteReturnFirsOrDefault<BomDetailModel>(proc, param);
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

        public Task<ResponseModel<BomDetailModel>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<BomDetailModel>> Modify(BomDetailModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<BomDetailModel>>> SearchActive(string keyWord)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<BomDetailModel>>> SearchAll(string keyWord)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<IList<BomDetailModel>>> SearchActiveByBomId(int bomId, string keyWord)
        {
            var returnData = new ResponseModel<IList<BomDetailModel>>();
            string proc = "usp_BomDetail_SearchActiveByBomId";
            var param = new DynamicParameters();
            param.Add("@BomId", bomId);
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<BomDetailModel>(proc, param);
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

        public async Task<ResponseModel<IList<BomDetailModel>>> SearchAllByBomId(int bomId, string keyWord)
        {
            var returnData = new ResponseModel<IList<BomDetailModel>>();
            string proc = "usp_BomDetail_SearchAllByBomId";
            var param = new DynamicParameters();
            param.Add("@BomId", bomId);
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<BomDetailModel>(proc, param);
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