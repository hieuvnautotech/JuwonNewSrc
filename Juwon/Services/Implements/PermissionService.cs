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
    public class PermissionService : IPermissionService
    {
        private readonly IRepository repository;

        public PermissionService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<ResponseModel<Permission>> Create(Permission model)
        {
            var returnData = new ResponseModel<Permission>();

            string proc = "p_PermissionDAO_Create";
            var param = new DynamicParameters();
            param.Add("@name", model.Name);
            param.Add("@permissionCategory", model.PermissionCategory);
            param.Add("@description", model.Description);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case 0:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        break;
                    case 1:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        break;
                    case 2:
                        proc = "p_PermissionDAO_GetByName";
                        param = new DynamicParameters();
                        param.Add("@name", model.Name);
                        var data = await repository.ExecuteReturnFirsOrDefault<Permission>(proc, param);
                        returnData.ResponseMessage = Resource.SUCCESS_Create;
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

        public async Task<ResponseModel<Permission>> DeleteByID(int ID)
        {
            var returnData = new ResponseModel<Permission>();

            string proc = "p_PermissionDAO_DeleteByID";
            var param = new DynamicParameters();
            param.Add("@ID", ID);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case 0:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        returnData.IsSuccess = false;
                        break;
                    case 1:
                        returnData.ResponseMessage = Resource.SUCCESS_Delete;
                        returnData.IsSuccess = false;
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

        public async Task<ResponseModel<IList<PermissionModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<PermissionModel>>();
            string proc = "p_PermissionDAO_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<PermissionModel>(proc);
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

        public async Task<ResponseModel<IList<PermissionModel>>> GetAllExceptRoot()
        {
            var returnData = new ResponseModel<IList<PermissionModel>>();
            string proc = "p_PermissionDAO_GetAllExceptRoot";
            try
            {
                var result = await repository.ExecuteReturnList<PermissionModel>(proc);
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

        public async Task<ResponseModel<PermissionModel>> GetByID(int Id)
        {
            var returnData = new ResponseModel<PermissionModel>();
            string proc = "p_PermissionDAO_GetByID";
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<PermissionModel>(proc, param);
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

        public async Task<ResponseModel<PermissionModel>> GetByName(string name)
        {
            var returnData = new ResponseModel<PermissionModel>();
            string proc = "p_PermissionDAO_GetByName";
            var param = new DynamicParameters();
            param.Add("@name", name);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<PermissionModel>(proc, param);
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

        public async Task<ResponseModel<Permission>> Modify(Permission model)
        {
            var returnData = new ResponseModel<Permission>();

            string proc = "p_PermissionDAO_Modify";
            var param = new DynamicParameters();
            param.Add("@id", model.ID);
            param.Add("@name", model.Name);
            param.Add("@permissionCategory", model.PermissionCategory);
            param.Add("@description", model.Description);
            param.Add("@active", model.Active);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
                switch (result)
                {
                    case 0:
                        returnData.ResponseMessage = Resource.ERROR_NotFound;
                        break;
                    case 2:
                        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                        break;
                    case 1:
                        proc = "p_PermissionDAO_GetByName";
                        param = new DynamicParameters();
                        param.Add("@name", model.Name);
                        var data = await repository.ExecuteReturnFirsOrDefault<Permission>(proc, param);
                        returnData.ResponseMessage = Resource.SUCCESS_Create;
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

        public async Task<ResponseModel<IList<PermissionModel>>> Search(string keyWord)
        {
            var returnData = new ResponseModel<IList<PermissionModel>>();
            string proc = "p_PermissionDAO_Search";
            var param = new DynamicParameters();
            param.Add("@name", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<PermissionModel>(proc, param);
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
    }
}