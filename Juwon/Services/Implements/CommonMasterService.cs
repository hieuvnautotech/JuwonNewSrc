using Dapper;
using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using Library;
using Library.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juwon.Services.Implements
{
    public class CommonMasterService : ICommonMasterService
    {
        private readonly IRepository _repository;

        public CommonMasterService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<CommonMaster>> Create(CommonMaster model)
        {
            string proc = "p_CommonMasterDAO_Create";
            var param = new DynamicParameters();
            param.Add("@Code", model.Code);
            param.Add("@Name", model.Name);

            var returnData = new ResponseModel<CommonMaster>();
            try
            {
                //var result = await DapperORM.ExecuteReturnScalar<int>(proc, param);
                var result = await _repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -1:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedCode;
                        returnData.Data = null;
                        returnData.HttpResponseCode = 500;
                        break;
                    case 0:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedCode;
                        returnData.Data = null;
                        break;
                    case 2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        returnData.Data = null;
                        break;
                    default:
                        proc = "p_CommonMasterDAO_GetByCode";
                        param = new DynamicParameters();
                        param.Add("@Code", model.Code);
                        //var data = await DapperORM.ExecuteReturnFirsOrDefault<CommonMaster>(proc, param);
                        var data = await _repository.ExecuteReturnFirsOrDefault<CommonMaster>(proc, param);
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.Data = data;
                        returnData.IsSuccess = true;
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<CommonMaster>> DeleteByID(int ID)
        {
            var returnData = new ResponseModel<CommonMaster>();
            string proc = "p_CommonMasterDAO_DeleteByID";
            var param = new DynamicParameters();
            param.Add("@Code", ID);
            try
            {
                //var result = await DapperORM.ExecuteReturnScalar<int>(proc, param);
                var result = await _repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -1:
                        returnData.ResponseMessage = Resource.ERROR_SystemError;
                        returnData.Data = null;
                        break;
                    case 0:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        returnData.Data = null;
                        break;
                    default:
                        returnData.ResponseMessage = Resource.SUCCESS_Delete;
                        returnData.Data = null;
                        returnData.IsSuccess = true;
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<IList<CommonMaster>>> GetAll()
        {
            string proc = "p_CommonMasterDAO_GetAll";
            var returnData = new ResponseModel<IList<CommonMaster>>();
            try
            {
                // var result = await DapperORM.ExecuteReturnList<CommonMaster>(proc);
                var result = await _repository.ExecuteReturnList<CommonMaster>(proc);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                }
                else
                {
                    returnData.Data = null;
                }
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<IList<CommonMaster>>> GetAllActive()
        {
            string proc = "p_CommonMaster_GetAllActive";
            var returnData = new ResponseModel<IList<CommonMaster>>();
            try
            {
                // var result = await DapperORM.ExecuteReturnList<CommonMaster>(proc);
                var result = await _repository.ExecuteReturnList<CommonMaster>(proc);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                }
                else
                {
                    returnData.Data = null;
                }
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<CommonMaster>> GetByCode(string code)
        {
            string proc = "p_CommonMasterDAO_GetByCode";
            var returnData = new ResponseModel<CommonMaster>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Code", code);
                // var result = await DapperORM.ExecuteReturnFirsOrDefault<CommonMaster>(proc, param);
                var result = await _repository.ExecuteReturnFirsOrDefault<CommonMaster>(proc, param);
                returnData.Data = result;
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<CommonMaster>> GetByID(int Id)
        {
            string proc = "p_CommonMasterDAO_GetByCode";
            var returnData = new ResponseModel<CommonMaster>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                // var result = await DapperORM.ExecuteReturnFirsOrDefault<CommonMaster>(proc, param);
                var result = await _repository.ExecuteReturnFirsOrDefault<CommonMaster>(proc, param);
                returnData.Data = result;
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<CommonMaster>> Modify(CommonMaster model)
        {
            string proc = "p_CommonMasterDAO_Modify";
            var param = new DynamicParameters();
            param.Add("@Code", model.Code);
            param.Add("@Name", model.Name);

            var returnData = new ResponseModel<CommonMaster>();
            try
            {
                // var result = await DapperORM.ExecuteReturnScalar<int>(proc, param);
                var result = await _repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case -1:
                        returnData.ResponseMessage = Resource.ERROR_SystemError;
                        returnData.Data = null;
                        returnData.HttpResponseCode = 500;
                        break;
                    case 0:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        returnData.Data = null;
                        break;
                    case 2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        returnData.Data = null;
                        break;
                    default:
                        proc = "p_CommonMasterDAO_GetByCode";
                        param = new DynamicParameters();
                        param.Add("@Code", model.Code);
                        //var data = await DapperORM.ExecuteReturnFirsOrDefault<CommonMaster>(proc, param);
                        var data = await _repository.ExecuteReturnFirsOrDefault<CommonMaster>(proc, param);
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.Data = data;
                        returnData.IsSuccess = true;
                        break;
                }
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<IList<CommonMaster>>> Search(string keyWord)
        {
            var returnData = new ResponseModel<IList<CommonMaster>>();
            string proc = "p_CommonMasterDAO_Search";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                //var result = await DapperORM.ExecuteReturnList<CommonMaster>(proc, param);
                var result = await _repository.ExecuteReturnList<CommonMaster>(proc, param);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                }
                else
                {
                    returnData.Data = null;
                }
                returnData.ResponseMessage = Resource.SUCCESS_Success;
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
