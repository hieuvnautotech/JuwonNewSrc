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
    public class QCDetailService : IQCDetailService
    {
        private readonly IRepository repository;

        public QCDetailService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<QCDetail>> Create(QCDetail model)
        {
            var returnData = new ResponseModel<QCDetail>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_QCDetail_Create";
            var param = new DynamicParameters();
            param.Add("@QCDetailName", model.QCDetailName);
            param.Add("@QCDetailDescription", model.QCDetailDescription);
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
                       
                        model.QCDetailId = result;
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

        public async Task<ResponseModel<int>> Delete(int qCDetailId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_QCDetail_Delete";
            var param = new DynamicParameters();
            param.Add("@QCDetailId", qCDetailId);
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

        public async Task<ResponseModel<IList<QCDetail>>> GetActive()
        {
            var returnData = new ResponseModel<IList<QCDetail>>();
            string proc = $"usp_QCDetail_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<QCDetail>(proc);
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

        public async Task<ResponseModel<IList<QCDetail>>> GetAll()
        {
            var returnData = new ResponseModel<IList<QCDetail>>();
            string proc = $"usp_QCDetail_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<QCDetail>(proc);
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

        public Task<ResponseModel<QCDetail>> GetByName(string qCDetailName)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<QCDetail>> Modify(QCDetail model)
        {
            var returnData = new ResponseModel<QCDetail>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_QCDetail_Modify";
            var param = new DynamicParameters();
            param.Add("@QCDetailId", model.QCDetailId);
            param.Add("@QCDetailName", model.QCDetailName);
            param.Add("@QCDetailDescription", model.QCDetailDescription);
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
                        var data = await GetById(model.QCDetailId);
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

        public async Task<ResponseModel<QCDetail>> GetById(int qCDetailId)
        {
            var returnData = new ResponseModel<QCDetail>();
            string proc = "usp_QCDetail_GetById";
            var param = new DynamicParameters();
            param.Add("@QCDetailId", qCDetailId);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<QCDetail>(proc, param);
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

        public async Task<ResponseModel<IList<QCDetail>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<QCDetail>>();
            string proc = $"usp_QCDetail_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<QCDetail>(proc, param);
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

        public async Task<ResponseModel<IList<QCDetail>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<QCDetail>>();
            string proc = $"usp_QCDetail_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<QCDetail>(proc, param);
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

        public async Task<ResponseModel<IList<QCDetail>>> GetAllByQCMasterId(int qcMasterId)
        {
            var returnData = new ResponseModel<IList<QCDetail>>();
            string proc = $"usp_QCDetail_GetAllByQCMasterId";
            var param = new DynamicParameters();
            param.Add("@QCMasterId", qcMasterId);
            try
            {
                var result = await repository.ExecuteReturnList<QCDetail>(proc, param);
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

        public async Task<ResponseModel<IList<QCDetail>>> GetActiveByQCMasterId(int qcMasterId)
        {
            var returnData = new ResponseModel<IList<QCDetail>>();
            string proc = $"usp_QCDetail_GetActiveByQCMasterId";
            var param = new DynamicParameters();
            param.Add("@QCMasterId", qcMasterId);
            try
            {
                var result = await repository.ExecuteReturnList<QCDetail>(proc, param);
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

        public async Task<ResponseModel<int>> Active(int qcDetailId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_QCDetail_Active";
            var param = new DynamicParameters();
            param.Add("@QCDetailId", qcDetailId);
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
    }
}