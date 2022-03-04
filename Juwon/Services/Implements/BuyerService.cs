using Dapper;
using Library.Common;
using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library;
using Library.Helper;

namespace Juwon.Services.Implements
{
    public class BuyerService : IBuyerService
    {
        private readonly IRepository repository;

        public BuyerService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<ResponseModel<Buyer>> Create(Buyer model)
        {
            var returnData = new ResponseModel<Buyer>();

            if (string.IsNullOrWhiteSpace(model.BuyerCode) || string.IsNullOrWhiteSpace(model.BuyerName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            model.CreatedBy = SessionHelper.GetUserSession().ID;

            string proc = "usp_Buyer_Create";
            var param = new DynamicParameters();
            param.Add("@BuyerCode", model.BuyerCode);
            param.Add("@BuyerName", model.BuyerName);
            param.Add("@BuyerDescription", model.BuyerDescription);
            param.Add("@BuyerEmail", model.BuyerEmail);
            param.Add("@BuyerPhone", model.BuyerPhone);
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
                        model.BuyerId = result;
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
                throw;
            }
        }

        public async Task<ResponseModel<int>> Delete(int buyerId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Buyer_DeleteByID";
            var param = new DynamicParameters();
            param.Add("@BuyerId", buyerId);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        returnData.IsSuccess = false;
                        break;
                    case 1:
                        returnData.ResponseMessage = Resource.SUCCESS_Delete;
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

        public async Task<ResponseModel<int>> Active(int buyerId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Buyer_Active";
            var param = new DynamicParameters();
            param.Add("@BuyerId", buyerId);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        returnData.IsSuccess = false;
                        break;
                    case 1:
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
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

        public async Task<ResponseModel<IList<Buyer>>> GetAll()
        {
            var returnData = new ResponseModel<IList<Buyer>>();
            string proc = "usp_Buyer_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<Buyer>(proc);
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

        public async Task<ResponseModel<IList<Buyer>>> GetActive()
        {
            var returnData = new ResponseModel<IList<Buyer>>();
            string proc = "usp_Buyer_GetAllActive";
            try
            {
                var result = await repository.ExecuteReturnList<Buyer>(proc);
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

        public async Task<ResponseModel<IList<Buyer>>> GetAllByVendorId(int vendorId)
        {
            var returnData = new ResponseModel<IList<Buyer>>();
            string proc = "usp_Buyer_GetAllByVendorId";
            var param = new DynamicParameters();
            param.Add("@VendorId", vendorId);
            try
            {
                var result = await repository.ExecuteReturnList<Buyer>(proc, param);
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

        public async Task<ResponseModel<Buyer>> GetByCode(string buyerCode)
        {
            var returnData = new ResponseModel<Buyer>();
            string proc = $"usp_Vendor_GetByCode";
            var param = new DynamicParameters();
            param.Add("@BuyerCode", buyerCode);
            var data = await repository.ExecuteReturnFirsOrDefault<Buyer>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;

            }
            returnData.IsSuccess = true;

            return returnData;
        }

        public async Task<ResponseModel<Buyer>> GetById(int buyerId)
        {
            var returnData = new ResponseModel<Buyer>();
            string proc = $"usp_Buyer_GetById";
            var param = new DynamicParameters();
            param.Add("@BuyerId", buyerId);
            var data = await repository.ExecuteReturnFirsOrDefault<Buyer>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;

            }
            returnData.IsSuccess = true;

            return returnData;
        }

        public async Task<ResponseModel<Buyer>> GetByName(string buyerName)
        {
            var returnData = new ResponseModel<Buyer>();
            string proc = $"usp_Buyer_GetByName";
            var param = new DynamicParameters();
            param.Add("@BuyerName", buyerName);
            var data = await repository.ExecuteReturnFirsOrDefault<Buyer>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;

            }
            returnData.IsSuccess = true;

            return returnData;
        }

        public async Task<ResponseModel<IList<Buyer>>> GetByVendorId(int vendorId)
        {
            var returnData = new ResponseModel<IList<Buyer>>();
            string proc = "usp_Buyer_GetByVendorId";
            var param = new DynamicParameters();
            param.Add("@VendorId", vendorId);
            try
            {
                var result = await repository.ExecuteReturnList<Buyer>(proc, param);
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

        public async Task<ResponseModel<Buyer>> Modify(Buyer model)
        {
            var returnData = new ResponseModel<Buyer>();

            if (string.IsNullOrWhiteSpace(model.BuyerCode) || string.IsNullOrWhiteSpace(model.BuyerName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }
            model.ModifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Buyer_Modify";
            var param = new DynamicParameters();
            param.Add("@BuyerId", model.BuyerId);
            param.Add("@BuyerName", model.BuyerName);
            param.Add("@BuyerDescription", model.BuyerDescription);
            param.Add("@BuyerEmail", model.BuyerEmail);
            param.Add("@BuyerPhone", model.BuyerPhone);
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
                        var data = await GetById(model.BuyerId);
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

        public async Task<ResponseModel<IList<Buyer>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Buyer>>();
            string proc = "usp_Buyer_SearchAll";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Buyer>(proc, param);
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

        public async Task<ResponseModel<IList<Buyer>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Buyer>>();
            string proc = "usp_Buyer_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Buyer>(proc, param);
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
