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
    public class MoldService : IMoldService
    {
        private readonly IRepository repository;

        public MoldService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<Mold>> Create(Mold model)
        {
            string MoldCode = model.MoldCode.ToUpper() + "-00";
            var returnData = new ResponseModel<Mold>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Mold_Create";
            var param = new DynamicParameters();
            param.Add("@MoldCode", MoldCode);
            param.Add("@MoldName", model.MoldName);
            param.Add("@UnitPrice", model.UnitPrice);
            param.Add("@Currency", model.Currency);
            param.Add("@Image", model.Image);
            param.Add("@CreatedBy", createdBy);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -1:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedCode;
                        break;
                    case -2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        break;
                    case 0:
                        break;
                    default:
                        model.MoldId = result;
                        model.CreatedDate = DateTime.Now;
                        model.MoldCode = model.MoldCode.ToUpper() + "-00";
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

        public async Task<ResponseModel<int>> Delete(int moldId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Mold_Delete";
            var param = new DynamicParameters();
            param.Add("@MoldId", moldId);
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

        public async Task<ResponseModel<int>> Active(int moldId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Mold_Active";
            var param = new DynamicParameters();
            param.Add("@MoldId", moldId);
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

        public async Task<ResponseModel<IList<Mold>>> GetActive()
        {
            var returnData = new ResponseModel<IList<Mold>>();
            string proc = "usp_Mold_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<Mold>(proc);
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

        public async Task<ResponseModel<IList<Mold>>> GetAll()
        {
            var returnData = new ResponseModel<IList<Mold>>();
            string proc = "usp_Mold_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<Mold>(proc);
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

        public async Task<ResponseModel<Mold>> GetById(int moldId)
        {
            var returnData = new ResponseModel<Mold>();
            string proc = "usp_Mold_GetById";
            var param = new DynamicParameters();
            param.Add("@MoldId", moldId);
            var data = await repository.ExecuteReturnFirsOrDefault<Mold>(proc, param);
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

        public async Task<ResponseModel<Mold>> GetByName(string moldName)
        {
            var returnData = new ResponseModel<Mold>();
            string proc = "usp_Mold_GetByName";
            var param = new DynamicParameters();
            param.Add("@MoldName", moldName);
            var data = await repository.ExecuteReturnFirsOrDefault<Mold>(proc, param);
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

        public async Task<ResponseModel<Mold>> Modify(Mold model)
        {
            string MoldCode = model.MoldCode.ToUpper() + "-00";
            var returnData = new ResponseModel<Mold>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Mold_Modify";
            var param = new DynamicParameters();
            param.Add("@MoldId", model.MoldId);
            param.Add("@MoldCode", MoldCode);
            param.Add("@MoldName", model.MoldName);
            param.Add("@UnitPrice", model.UnitPrice);
            param.Add("@Currency", model.Currency);
            param.Add("@Image", model.Image);
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
                        var data = await GetById(model.MoldId);
                        returnData.ResponseMessage = Resource.SUCCESS_Modify;
                        model.MoldCode = model.MoldCode.ToUpper() + "-00";
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

        public async Task<ResponseModel<IList<Mold>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Mold>>();
            string proc = "usp_Mold_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Mold>(proc, param);
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

        public async Task<ResponseModel<IList<Mold>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Mold>>();
            string proc = "usp_Mold_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Mold>(proc, param);
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