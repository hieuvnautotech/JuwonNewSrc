using Dapper;
using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using Library;
using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Juwon.Services.Implements
{
    public class CommonDetailService : ICommonDetailService
    {
        private readonly IRepository repository;

        public CommonDetailService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<CommonDetailModel>> Create(CommonDetailModel model)
        {
            var returnData = new ResponseModel<CommonDetailModel>();

            string proc = "p_CommonDetailDAO_Create";
            var param = new DynamicParameters();
            param.Add("@Code", model.Code);
            param.Add("@Name", model.Name);
            param.Add("@MasterCode", model.MasterCode);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case 0:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedCode;
                        break;
                    case 2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        break;
                    case 1:
                        proc = "p_CommonDetailDAO_GetByCodeAndMasterCode";
                        param = new DynamicParameters();
                        param.Add("@Code", model.Code);
                        param.Add("@MasterCode", model.MasterCode);
                        var data = await repository.ExecuteReturnFirsOrDefault<CommonDetailModel>(proc, param);
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.Data = data;
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

        public Task<ResponseModel<CommonDetailModel>> Delete(CommonDetailModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<int>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<CommonDetailModel>>> GetActive()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<IList<CommonDetailModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<CommonDetailModel>>();
            string proc = "p_CommonDetailDAO_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<CommonDetailModel>(proc);
                if (result.Count > 0)
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

        public async Task<ResponseModel<IList<CommonDetailModel>>> GetAllByMasterCode(string masterCode)
        {
            var returnData = new ResponseModel<IList<CommonDetailModel>>();
            string proc = "p_CommonDetailDAO_GetAllByMasterCode";
            var param = new DynamicParameters();
            param.Add("@MasterCode", masterCode);
            try
            {
                var result = await repository.ExecuteReturnList<CommonDetailModel>(proc, param);
                if (result.Count > 0)
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

        public async Task<ResponseModel<CommonDetailModel>> GetByCodeAndMasterCode(string code, string masterCode)
        {
            var returnData = new ResponseModel<CommonDetailModel>();
            string proc = "p_CommonDetailDAO_GetByCodeAndMasterCode";
            var param = new DynamicParameters();
            param.Add("@Code", code);
            param.Add("@MasterCode", masterCode);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<CommonDetailModel>(proc, param);
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

        public async Task<ResponseModel<CommonDetailModel>> GetById(int Id)
        {
            var returnData = new ResponseModel<CommonDetailModel>();
            string proc = "p_CommonDetailDAO_GetByID";
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<CommonDetailModel>(proc, param);
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

        public Task<ResponseModel<CommonDetailModel>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<CommonDetailModel>> Modify(CommonDetail model)
        {
            var returnData = new ResponseModel<CommonDetailModel>();

            string proc = "p_CommonDetailDAO_Modify";
            var param = new DynamicParameters();
            param.Add("@Code", model.Code);
            param.Add("@Name", model.Name);
            param.Add("@MasterCode", model.MasterCode);
            param.Add("@Active", model.Active);
            try
            {
                var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case 0:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        break;
                    case 2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        break;
                    case 1:
                        proc = "p_CommonDetailDAO_GetByCodeAndMasterCode";
                        param = new DynamicParameters();
                        param.Add("@Code", model.Code);
                        param.Add("@MasterCode", model.MasterCode);
                        var data = await repository.ExecuteReturnFirsOrDefault<CommonDetailModel>(proc, param);
                        returnData.ResponseMessage = Resource.SUCCESS_Success;
                        returnData.Data = data;
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

        public Task<ResponseModel<CommonDetailModel>> Modify(CommonDetailModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<IList<CommonDetailModel>>> Search(string keyWord, string MasterCode)
        {
            var returnData = new ResponseModel<IList<CommonDetailModel>>();
            string proc = "p_CommonDetailDAO_Search";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            param.Add("@MasterCode", MasterCode);
            try
            {
                var result = await repository.ExecuteReturnList<CommonDetailModel>(proc, param);
                if (result.Count > 0)
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

        public Task<ResponseModel<IList<CommonDetailModel>>> SearchActive(string keyWord)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<CommonDetailModel>>> SearchAll(string keyWord)
        {
            throw new NotImplementedException();
        }
    }
}