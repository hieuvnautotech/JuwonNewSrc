using Dapper;
using Juwon.Models;
using Juwon.Models.DTOs;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using Library;
using Library.Common;
using Library.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Juwon.Services.Implements
{
    public class QCMasterService : IQCMasterService
    {
        private readonly IRepository repository;
        private readonly IQCDetailService qcDetailService;

        public QCMasterService(IRepository IRepository, IQCDetailService IQCDetailService)
        {
            repository = IRepository;
            qcDetailService = IQCDetailService;
        }

        public async Task<ResponseModel<QCMasterModel>> Create(QCMasterModel model)
        {
            var returnData = new ResponseModel<QCMasterModel>();
            model.CreatedBy = SessionHelper.GetUserSession().ID;
            var qcDetailIds = JsonConvert.SerializeObject(model.QCDetailIds);
            string proc = $"usp_QCMaster_Create";
            var param = new DynamicParameters();
            param.Add("@QCMasterName", model.QCMasterName);
            param.Add("@QCMasterDescription", model.QCMasterDescription);
            param.Add("@CreatedBy", model.CreatedBy);
            param.Add("@QCDetailIds", qcDetailIds);
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
                        model.QCMasterId = result;
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
            }
        }

        public async Task<ResponseModel<int>> Delete(int id)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_QCMaster_Delete";
            var param = new DynamicParameters();
            param.Add("@QCMasterId", id);
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

        public async Task<ResponseModel<int>> Active(int id)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_QCMaster_Active";
            var param = new DynamicParameters();
            param.Add("@QCMasterId", id);
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

        public async Task<ResponseModel<IList<QCMasterModel>>> GetActive()
        {
            var returnData = new ResponseModel<IList<QCMasterModel>>();
            string proc = $"usp_QCMaster_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<QCMasterModel>(proc);
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

        public async Task<ResponseModel<IList<QCMasterModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<QCMasterModel>>();
            string proc = $"usp_QCMaster_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<QCMasterModel>(proc);
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

        public async Task<ResponseModel<QCMasterModel>> GetById(int qcMasterId)
        {
            var returnData = new ResponseModel<QCMasterModel>();
            string proc = "usp_QCMaster_GetById";
            var param = new DynamicParameters();
            param.Add("@QCMasterId", qcMasterId);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<QCMasterModel>(proc, param);
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

        public async Task<ResponseModel<QCMasterModel>> GetByName(string qcMasterName)
        {
            var returnData = new ResponseModel<QCMasterModel>();
            string proc = "usp_QCMaster_GetById";
            var param = new DynamicParameters();
            param.Add("@QCMasterName", qcMasterName);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<QCMasterModel>(proc, param);
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

        public async Task<ResponseModel<QCMasterModel>> Modify(QCMasterModel model)
        {
            model.ModifiedBy = SessionHelper.GetUserSession().ID;
            var returnData = new ResponseModel<QCMasterModel>();

            //tìm ra list QCDetail đã có trong bảng QCRel theo QCMasterId
            var qcDetailList = await qcDetailService.GetAllByQCMasterId(model.QCMasterId);
            var existedIds = new List<int?>();

            if (qcDetailList.Data != null)
            {
                foreach (var item in qcDetailList.Data)
                {
                    existedIds.Add(item.QCDetailId);
                }
            }

            var receivedIds = new List<int?>();
            var existedNeedActive = new List<int?>();
            var existedNeedDeActive = new List<int?>();
            var needInsert = new List<int?>();
            if (model.QCDetailIds != null)
            {
                foreach (var item in model.QCDetailIds)
                {
                    receivedIds.Add(item);
                }

                var combine = existedIds.Union(receivedIds).ToList();

                //Tìm ds cần Update thành Active
                existedNeedActive = existedIds.Intersect(receivedIds).ToList();

                //Tìm ds không có trong ds cần Update thành Active
                var nonExisted = combine.Except(existedNeedActive).ToList();

                //Tìm ds cần Update thành In-Active
                existedNeedDeActive = existedIds.Intersect(nonExisted).ToList();

                //Tìm ds cần Insert
                needInsert = nonExisted.Except(existedNeedDeActive).ToList();
            }

            var json_ExistedNeedActive = JsonConvert.SerializeObject(existedNeedActive);
            var json_ExistedNeedDeActive = JsonConvert.SerializeObject(existedNeedDeActive);
            var json_NeedInsert = JsonConvert.SerializeObject(needInsert);

            if (string.IsNullOrWhiteSpace(model.QCMasterName))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
                return returnData;
            }

            string proc = "usp_QCMaster_Modify";
            var param = new DynamicParameters();
            param.Add("@QCMasterId", model.QCMasterId);
            param.Add("@QCMasterName", model.QCMasterName);
            param.Add("@QCMasterDescription", model.QCMasterDescription);
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
                        var data = await GetById(model.QCMasterId);
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

        public async Task<ResponseModel<IList<QCMasterModel>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<QCMasterModel>>();
            string proc = $"usp_QCMaster_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<QCMasterModel>(proc, param);
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

        public async Task<ResponseModel<IList<QCMasterModel>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<QCMasterModel>>();
            string proc = $"usp_QCMaster_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<QCMasterModel>(proc, param);
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
    }
}