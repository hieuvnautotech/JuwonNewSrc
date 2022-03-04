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
    public class VendorCategoryService : IVendorCategoryService
    {
        private readonly IRepository repository;

        public VendorCategoryService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<ResponseModel<VendorCategory>> Create(VendorCategory model)
        {
            var returnData = new ResponseModel<VendorCategory>();

            //Vendor Category Code & Name cannot be blank
            if (string.IsNullOrWhiteSpace(model.VendorCategoryName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_VendorCategory_Create";
            var param = new DynamicParameters();
            param.Add("@VendorCategoryName", model.VendorCategoryName);
            param.Add("@CreatedBy", model.CreatedBy);
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
                        model.VendorCategoryId = result;
                        model.CreatedDate = DateTime.Now;
                        model.Active = true;
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

        public async Task<ResponseModel<int>> Delete(int vendorCategoryId)
        {

            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_VendorCategory_DeleteByID";
            var param = new DynamicParameters();
            param.Add("@VendorCategoryId", vendorCategoryId);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
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

        public async Task<ResponseModel<IList<VendorCategory>>> GetAll()
        {
            var returnData = new ResponseModel<IList<VendorCategory>>();
            string proc = "usp_VendorCategory_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<VendorCategory>(proc);
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

        public async Task<ResponseModel<IList<VendorCategory>>> GetActive()
        {
            var returnData = new ResponseModel<IList<VendorCategory>>();
            string proc = "usp_VendorCategory_GetAllActive";
            try
            {
                var result = await repository.ExecuteReturnList<VendorCategory>(proc);
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

        public async Task<ResponseModel<VendorCategory>> GetById(int id)
        {
            var returnData = new ResponseModel<VendorCategory>();
            string proc = "usp_VendorCategory_GetById";
            var param = new DynamicParameters();
            param.Add("@VendorCategoryId", id);
            var data = await repository.ExecuteReturnFirsOrDefault<VendorCategory>(proc, param);
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

        public async Task<ResponseModel<VendorCategory>> GetByName(string code)
        {
            var returnData = new ResponseModel<VendorCategory>();
            string proc = "usp_VendorCategory_GetByName";
            var param = new DynamicParameters();
            param.Add("@VendorCategoryName", code);
            var data = await repository.ExecuteReturnFirsOrDefault<VendorCategory>(proc, param);
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

        public async Task<ResponseModel<VendorCategory>> Modify(VendorCategory model)
        {
            var returnData = new ResponseModel<VendorCategory>();

            //Process Code & Name cannot be blank
            if (string.IsNullOrWhiteSpace(model.VendorCategoryName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_VendorCategory_Modify";
            var param = new DynamicParameters();
            param.Add("@VendorCategoryId", model.VendorCategoryId);
            param.Add("@VendorCategoryName", model.VendorCategoryName);
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
                        var data = await GetById(model.VendorCategoryId);
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

        public async Task<ResponseModel<IList<VendorCategory>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<VendorCategory>>();
            string proc = "usp_VendorCategory_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<VendorCategory>(proc, param);
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

        public async Task<ResponseModel<IList<VendorCategory>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<VendorCategory>>();
            string proc = "usp_VendorCategory_SearchAll";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<VendorCategory>(proc, param);
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

        public async Task<ResponseModel<int>> Active(int vendorCategoryId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_VendorCategory_Active";
            var param = new DynamicParameters();
            param.Add("@VendorCategoryId", vendorCategoryId);
            param.Add("@ModifiedBy", modifiedBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -3:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
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
    }
}
