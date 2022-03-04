using Dapper;
using Library.Common;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Models;
using Library;
using Library.Helper;

namespace Juwon.Services.Implements
{
    public class ColorService : IColorService
    {
        private readonly IRepository repository;

        public ColorService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<ResponseModel<Color>> Create(Color model)
        {
            var returnData = new ResponseModel<Color>();

            if (string.IsNullOrWhiteSpace(model.ColorCode) || string.IsNullOrWhiteSpace(model.ColorName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_Color_Create";
            var param = new DynamicParameters();
            param.Add("@ColorCode", model.ColorCode);
            param.Add("@ColorName", model.ColorName);
            param.Add("@ColorDescription", model.ColorDescription);

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
                    case 1:
                        var data = await GetByCode(model.ColorCode);
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

        public async Task<ResponseModel<int>> Delete(int colorId)
        {

            var returnData = new ResponseModel<int>();
            var modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Color_DeleteByID";
            var param = new DynamicParameters();
            param.Add("@ColorId", colorId);
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

        public async Task<ResponseModel<int>> Active(int colorId)
        {

            var returnData = new ResponseModel<int>();
            var modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = "usp_Color_Active";
            var param = new DynamicParameters();
            param.Add("@ColorId", colorId);
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

        public async Task<ResponseModel<IList<Color>>> GetActive()
        {
            var returnData = new ResponseModel<IList<Color>>();
            string proc = "usp_Color_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<Color>(proc);
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

        public async Task<ResponseModel<IList<Color>>> GetAll()
        {
            var returnData = new ResponseModel<IList<Color>>();
            string proc = "usp_Color_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<Color>(proc);
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

        public async Task<ResponseModel<Color>> GetByCode(string code)
        {
            var returnData = new ResponseModel<Color>();
            string proc = "usp_Color_GetByCode";
            var param = new DynamicParameters();
            param.Add("@Colorcode", code);
            var data = await repository.ExecuteReturnFirsOrDefault<Color>(proc, param);
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

        public async Task<ResponseModel<Color>> GetById(int id)
        {
            var returnData = new ResponseModel<Color>();
            string proc = "usp_Color_GetById";
            var param = new DynamicParameters();
            param.Add("@ColorId", id);
            var data = await repository.ExecuteReturnFirsOrDefault<Color>(proc, param);
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

        public Task<ResponseModel<Color>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Color>> Modify(Color model)
        {
            var returnData = new ResponseModel<Color>();

            if (string.IsNullOrWhiteSpace(model.ColorCode) || string.IsNullOrWhiteSpace(model.ColorName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_Color_Modify";
            var param = new DynamicParameters();
            param.Add("@ColorId", model.ColorId);
            param.Add("@ColorName", model.ColorName);
            param.Add("@ColorDescription", model.ColorDescription);
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
                        var data = await GetById(model.ColorId);
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

        public async Task<ResponseModel<IList<Color>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Color>>();
            string proc = "usp_Color_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Color>(proc, param);
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

        public async Task<ResponseModel<IList<Color>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Color>>();
            string proc = "usp_Color_SearchAll";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Color>(proc, param);
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
