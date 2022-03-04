using Dapper;
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
    public class MaterialService : IMaterialService
    {
        private readonly IRepository repository;

        public MaterialService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<int>> Active(int materialId)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Material_Active";
            var param = new DynamicParameters();
            param.Add("@MaterialId", materialId);
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

        public async Task<ResponseModel<MaterialModel>> Create(MaterialModel model)
        {
            var returnData = new ResponseModel<MaterialModel>();
            model.CreatedBy = SessionHelper.GetUserSession().ID;
            if (string.IsNullOrWhiteSpace(model.MaterialName) || string.IsNullOrWhiteSpace(model.MaterialRemark))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
            }
            else
            {
                if (model.MaterialRemark.Length == 5)
                {
                    model.MaterialRemark = model.MaterialRemark.ToUpper();
                    var postJson = JsonConvert.SerializeObject(model);

                    string proc = $"usp_Material_Create";
                    var param = new DynamicParameters();
                    param.Add("@MaterialModel", postJson);
                    try
                    {
                        var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                        switch (result)
                        {
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
                    }
                    catch (Exception)
                    {
                        returnData.HttpResponseCode = 500;
                    }  
                }
                else
                {
                    returnData.ResponseMessage = Resource.ERROR_CodeInvalid;
                }
            }
            return returnData;
        }

        public async Task<ResponseModel<int>> Delete(int id)
        {
            var returnData = new ResponseModel<int>();
            int modifiedBy = SessionHelper.GetUserSession().ID;
            string proc = $"usp_Material_Delete";
            var param = new DynamicParameters();
            param.Add("@MaterialId", id);
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

        public async Task<ResponseModel<IList<MaterialModel>>> GetActive()
        {
            var returnData = new ResponseModel<IList<MaterialModel>>();
            string proc = $"usp_Material_GetActive";
            try
            {
                var result = await repository.ExecuteReturnList<MaterialModel>(proc);
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

        public async Task<ResponseModel<IList<MaterialModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<MaterialModel>>();
            string proc = $"usp_Material_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<MaterialModel>(proc);
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

        public async Task<ResponseModel<MaterialModel>> GetByCode(string materialCode)
        {
            var returnData = new ResponseModel<MaterialModel>();
            string proc = "usp_Material_GetByCode";
            var param = new DynamicParameters();
            param.Add("@MaterialCode", materialCode);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<MaterialModel>(proc, param);
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

        public async Task<ResponseModel<MaterialModel>> GetById(int id)
        {
            var returnData = new ResponseModel<MaterialModel>();
            string proc = "usp_Material_GetById";
            var param = new DynamicParameters();
            param.Add("@MaterialId", id);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<MaterialModel>(proc, param);
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

        public Task<ResponseModel<MaterialModel>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<MaterialModel>> Modify(MaterialModel model)
        {
            var returnData = new ResponseModel<MaterialModel>();
            model.ModifiedBy = SessionHelper.GetUserSession().ID;
            if (string.IsNullOrWhiteSpace(model.MaterialName) || string.IsNullOrWhiteSpace(model.MaterialRemark))
            {
                returnData.ResponseMessage = Resource.ERROR_FullFillTheForm;
            }
            else
            {
                if (model.MaterialRemark.Length == 5)
                {
                    model.MaterialRemark = model.MaterialRemark.ToUpper();
                    var postJson = JsonConvert.SerializeObject(model);

                    string proc = $"usp_Material_Modify";
                    var param = new DynamicParameters();
                    param.Add("@MaterialModel", postJson);
                    try
                    {
                        var result = await repository.Write_ExecuteReturnScalar<int>(proc, param);
                        switch (result)
                        {
                            case -1:
                                returnData.ResponseMessage = Resource.ERROR_DuplicatedCode;
                                break;
                            case 0:
                                break;
                            default:
                                var data = await GetById(result);
                                returnData.ResponseMessage = Resource.SUCCESS_Modify;
                                returnData.Data = data.Data;
                                returnData.IsSuccess = true;
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        returnData.HttpResponseCode = 500;
                    } 
                }
                else
                {
                    returnData.ResponseMessage = Resource.ERROR_CodeInvalid;
                }
            }
            return returnData;
        }

        public async Task<ResponseModel<IList<MaterialModel>>> SearchActive(string keyWord)
        {
            var returnData = new ResponseModel<IList<MaterialModel>>();
            string proc = $"usp_Material_SearchActive";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<MaterialModel>(proc);
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

        public async Task<ResponseModel<IList<MaterialModel>>> SearchAll(string keyWord)
        {
            var returnData = new ResponseModel<IList<MaterialModel>>();
            string proc = $"usp_Material_SearchAll";
            var param = new DynamicParameters();
            param.Add("@KeyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<MaterialModel>(proc);
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