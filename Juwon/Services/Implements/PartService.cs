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
    public class PartService : IPartService
    {
        private readonly IRepository repository;

        public PartService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<Part>> Create(Part model)
        {
            var returnData = new ResponseModel<Part>();
            int createdBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Part_Create";
            var param = new DynamicParameters();
            param.Add("@PartCode", model.PartCode);
            param.Add("@PartName", model.PartName);
            param.Add("@PartDescription", model.PartDescription);
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
                        model.PartId = result;
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

        public async Task<ResponseModel<int>> Delete(int partId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Part_Delete";
            var param = new DynamicParameters();
            param.Add("@PartId", partId);
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

        public async Task<ResponseModel<int>> Active(int partId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Part_Active";
            var param = new DynamicParameters();
            param.Add("@PartId", partId);
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

        public async Task<ResponseModel<IList<Part>>> GetActive()
        {
            var returnData = new ResponseModel<IList<Part>>();
            string proc = $"usp_Part_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<Part>(proc);
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

        public async Task<ResponseModel<IList<Part>>> GetAll()
        {
            var returnData = new ResponseModel<IList<Part>>();
            string proc = $"usp_Part_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<Part>(proc);
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
            }
        }

        public async Task<ResponseModel<Part>> GetById(int partId)
        {
            var returnData = new ResponseModel<Part>();
            string proc = "usp_Part_GetById";
            var param = new DynamicParameters();
            param.Add("@PartId", partId);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<Part>(proc, param);
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

        public async Task<ResponseModel<Part>> GetByCode(string partCode)
        {
            var returnData = new ResponseModel<Part>();
            string proc = "usp_Part_GetByCode";
            var param = new DynamicParameters();
            param.Add("@PartCode", partCode);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<Part>(proc, param);
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

        public async Task<ResponseModel<Part>> GetByName(string partName)
        {
            var returnData = new ResponseModel<Part>();
            string proc = "usp_Part_GetByName";
            var param = new DynamicParameters();
            param.Add("@PartName", partName);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<Part>(proc, param);
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

        public async Task<ResponseModel<Part>> Modify(Part model)
        {
            var returnData = new ResponseModel<Part>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Part_Modify";
            var param = new DynamicParameters();
            param.Add("@PartId", model.PartId);
            param.Add("@PartCode", model.PartCode);
            param.Add("@PartName", model.PartName);
            param.Add("@PartDescription", model.PartDescription);
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
                        var data = await GetById(model.PartId);
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

        public async Task<ResponseModel<IList<Part>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<Part>>();
            string proc = $"usp_Part_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Part>(proc, param);
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

        public async Task<ResponseModel<IList<Part>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<Part>>();
            string proc = $"usp_Part_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<Part>(proc, param);
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