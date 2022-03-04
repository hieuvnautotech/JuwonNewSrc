using Dapper;
using Library.Common;
using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library;
using Juwon.Models.DTOs;
using Library.Helper;
using Newtonsoft.Json;
using System.Linq;

namespace Juwon.Services.Implements
{
    public class VendorService : IVendorService
    {
        private readonly IRepository repository;
        private readonly IBuyerService buyerService;

        public VendorService(IRepository iRepository, IBuyerService IBuyerService)
        {
            repository = iRepository;
            buyerService = IBuyerService;
        }

        public async Task<ResponseModel<VendorModel>> Create(VendorModel model)
        {
            var returnData = new ResponseModel<VendorModel>();

            //Process Code & Name cannot be blank
            if (string.IsNullOrWhiteSpace(model.VendorCode) || string.IsNullOrWhiteSpace(model.VendorName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            var CreatedBy = SessionHelper.GetUserSession().ID;
            model.CreatedBy = CreatedBy;
            foreach (var item in model.Buyers)
            {
                item.CreatedBy = CreatedBy;
                item.CreatedDate = DateTime.Now;
            }

            var json = JsonConvert.SerializeObject(model.Buyers);

            string proc = "usp_Vendor_Create";
            var param = new DynamicParameters();
            param.Add("@VendorCode", model.VendorCode);
            param.Add("@VendorName", model.VendorName);
            param.Add("@VendorAddress", model.VendorAddress);
            param.Add("@VendorPhone", model.VendorPhone);
            param.Add("@DestinationId", model.DestinationId);
            param.Add("@VendorCategoryId", model.VendorCategoryId);
            param.Add("@CreatedBy", model.CreatedBy);
            param.Add("@Buyers", json);
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
                throw;
            }
        }

        public async Task<ResponseModel<int>> Delete(int vendorId)
        {

            var returnData = new ResponseModel<int>();
            int ModifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Vendor_DeleteByID";
            var param = new DynamicParameters();
            param.Add("@VendorId", vendorId);
            param.Add("@ModifiedBy", ModifiedBy);
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

        public async Task<ResponseModel<int>> Active(int vendorId)
        {

            var returnData = new ResponseModel<int>();
            int ModifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Vendor_Active";
            var param = new DynamicParameters();
            param.Add("@VendorId", vendorId);
            param.Add("@ModifiedBy", ModifiedBy);
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

        public Task<ResponseModel<IList<VendorModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<VendorModel>>> GetActive()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<VendorModel>> GetByCode(string code)
        {

            string proc = $"usp_Vendor_GetByCode";
            var param = new DynamicParameters();
            param.Add("@VendorCode", code);
            var data = await repository.ExecuteReturnFirsOrDefault<VendorModel>(proc, param);
            var returnData = new ResponseModel<VendorModel>();
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;

            }
            returnData.IsSuccess = true;

            return returnData;
        }

        public async Task<ResponseModel<VendorModel>> GetById(int Id)
        {
            string proc = $"usp_Vendor_GetById";
            var param = new DynamicParameters();
            param.Add("@VendorId", Id);
            var data = await repository.ExecuteReturnFirsOrDefault<VendorModel>(proc, param);
            var returnData = new ResponseModel<VendorModel>();
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;

            }
            returnData.IsSuccess = true;

            return returnData;
        }

        public async Task<ResponseModel<VendorModel>> Modify(VendorModel model)
        {
            model.ModifiedBy = SessionHelper.GetUserSession().ID;
            var returnData = new ResponseModel<VendorModel>();

            //tìm ra list Buyer đã có trong bảng VendorBuyerRel theo VendorId
            var buyerList = await buyerService.GetAllByVendorId(model.VendorId);
            var existedBuyerIds = new List<int>();

            if (buyerList.Data != null)
            {
                foreach (var buyer in buyerList.Data)
                {
                    existedBuyerIds.Add(buyer.BuyerId);
                }
            }

            var receivedBuyerIds = new List<int>();
            var existedNeedActive = new List<int>();
            var existedNeedDeActive = new List<int>();
            var needInsert = new List<int>();

            if (model.Buyers != null)
            {
                foreach (var item in model.Buyers)
                {
                    receivedBuyerIds.Add(item.BuyerId);
                }

                var combine = existedBuyerIds.Union(receivedBuyerIds).ToList();

                //so sánh với list Buyer nhận được từ phía client gửi lên
                /**
                 * Bước 1: lọc ra danh sách Buyer trùng (là ds trùng cần update thành Active) và không trùng
                 * Bước 2: từ danh sách Buyer không trùng tiếp tục đem so sánh với danh sách Buyer đã có trong bảng VendorBuyerRel để tìm ra danh sách Buyer trùng (là ds trùng cần update sang In-Active) và danh sách không trùng (là danh sách cần Insert)
                 */



                //Tìm ds cần Update thành Active
                existedNeedActive = existedBuyerIds.Intersect(receivedBuyerIds).ToList();

                //Tìm ds không có trong ds cần Update thành Active
                var nonExisted = combine.Except(existedNeedActive).ToList();

                //Tìm ds cần Update thành In-Active
                existedNeedDeActive = existedBuyerIds.Intersect(nonExisted).ToList();

                //Tìm ds cần Insert
                needInsert = nonExisted.Except(existedNeedDeActive).ToList(); 
            }

            var json_ExistedNeedActive = JsonConvert.SerializeObject(existedNeedActive);
            var json_ExistedNeedDeActive = JsonConvert.SerializeObject(existedNeedDeActive);
            var json_NeedInsert = JsonConvert.SerializeObject(needInsert);

            //Process Code & Name cannot be blank
            if (string.IsNullOrWhiteSpace(model.VendorCode) || string.IsNullOrWhiteSpace(model.VendorName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_Vendor_Modify";
            var param = new DynamicParameters();
            param.Add("@VendorId", model.VendorId);
            param.Add("@VendorName", model.VendorName);
            param.Add("@VendorAddress", model.VendorAddress);
            param.Add("@VendorPhone", model.VendorPhone);
            param.Add("@DestinationId", model.DestinationId);
            param.Add("@VendorCategoryId", model.VendorCategoryId);
            param.Add("@ModifiedBy", model.ModifiedBy);
            param.Add("@json_ExistedNeedActive", json_ExistedNeedActive);
            param.Add("@json_ExistedNeedDeActive", json_ExistedNeedDeActive);
            param.Add("@json_NeedInsert", json_NeedInsert);
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
                        var data = await GetById(model.VendorId);
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
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

        public async Task<ResponseModel<IList<VendorModel>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<VendorModel>>();
            string proc = "usp_Vendor_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<VendorModel>(proc, param);
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

        public async Task<ResponseModel<IList<VendorModel>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<VendorModel>>();
            string proc = "usp_Vendor_SearchAll";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<VendorModel>(proc, param);
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

        public Task<ResponseModel<VendorModel>> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
