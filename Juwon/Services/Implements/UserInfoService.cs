using Dapper;
using Library.Common;
using Juwon.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Services.Interfaces;
using Library;
using Juwon.Models;
using System;
using System.Text;

namespace Juwon.Services.Implements
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IRepository repository;
        public UserInfoService(IRepository iRepository)
        {
            repository = iRepository;
        }

        private string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        public async Task<UserModel> GetUserByUserName(string userName)
        {
            string proc = "p_UserDAO_GetUserByUserName";
            var param = new DynamicParameters();
            param.Add("@UserName", userName);
            var returnData = await repository.ExecuteReturnFirsOrDefault<UserModel>(proc, param);
            returnData.IpAddress = GetIp();
            returnData.Permissions = (List<string>)await GetUserPermissionsByUserName(userName);
            returnData.Roles = (List<string>)await GetUserRolesByUserName(userName);
            returnData.Menus = (List<MenuModel>)await GetUserMenusByUserName(userName);
            return returnData;
        }

        public async Task<IList<MenuModel>> GetUserMenusByUserName(string userName)
        {
            string proc = "p_UserDAO_GetUserMenusByUserName";
            var param = new DynamicParameters();
            param.Add("@UserName", userName);
            var result = await repository.ExecuteReturnList<MenuModel>(proc, param);
            foreach (var item in result)
            {
                item.MultiLang = CommonMethods.GetResourceTitle<Resource>(string.Concat("MML_", item.Name));
            }
            return CommonMethods.RemoveDuplicates(result);
        }

        public async Task<IList<string>> GetUserPermissionsByUserName(string userName)
        {
            string proc = "p_UserDAO_GetUserPermissionsByUserName";
            var param = new DynamicParameters();
            param.Add("@UserName", userName);
            var result = await repository.ExecuteReturnList<string>(proc, param);
            return CommonMethods.RemoveDuplicates(result);
        }

        public async Task<IList<string>> GetUserRolesByUserName(string userName)
        {
            string proc = "p_UserDAO_GetUserRolesByUserName";
            var param = new DynamicParameters();
            param.Add("@UserName", userName);
            var result = await repository.ExecuteReturnList<string>(proc, param);
            return CommonMethods.RemoveDuplicates(result);
        }

        public async Task<int> RecordUserLog(int userInfoId, string ipAddress, string sessionId)
        {
            string proc = "usp_RecordUserLog";
            var param = new DynamicParameters();
            param.Add("@UserInfoId", userInfoId);
            param.Add("@IpAddress", ipAddress);
            param.Add("@SessionId", sessionId);
            return await repository.ExecuteReturnScalar<int>(proc, param);
        }

        public async Task<int> ChangePassword(string userName, string password)
        {
            string proc = "p_UserDao_ChangePassword";
            var param = new DynamicParameters();
            param.Add("@UserName", userName);
            param.Add("@Password", password);
            return await repository.ExecuteReturnScalar<int>(proc, param);
        }

        public async Task<ResponseModel<IList<UserModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<UserModel>>();
            string proc = "p_UserDAO_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<UserModel>(proc);
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

        public async Task<ResponseModel<UserModel>> GetByID(int id)
        {
            var returnData = new ResponseModel<UserModel>();
            string proc = "p_UserDAO_GetByID";
            var param = new DynamicParameters();
            param.Add("@id", id);
            var data = await repository.ExecuteReturnFirsOrDefault<UserModel>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;
            }
            else
            {
                returnData.ResponseMessage = Resource.ERROR_NotFound;
            }
            returnData.IsSuccess = true;
            return returnData;
        }

        public async Task<ResponseModel<IList<UserModel>>> Search(string keyWord)
        {
            var returnData = new ResponseModel<IList<UserModel>>();
            string proc = "p_UserDAO_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<UserModel>(proc, param);
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

        public async Task<int> Create(UserInfo model)
        {
            //var returnData = new ResponseModel<int>();

            string proc = "p_UserDAO_Create";
            var param = new DynamicParameters();
            param.Add("@username", model.Username);
            param.Add("@password", model.Password);
            param.Add("@name", model.Name);
            param.Add("@email", model.Email);
            param.Add("@phone", model.Phone);
            param.Add("@address", model.Address);
            param.Add("@LocationID", 0);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
                //switch (result)
                //{
                //    case -2:
                //        returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                //        break;
                //    case -1:
                //        returnData.ResponseMessage = Resource.ERROR_DuplicatedCode;
                //        break;
                //    case 1:
                //        proc = "usp_Process_GetByCode";
                //        param = new DynamicParameters();
                //        param.Add("@ProcessCode", model.ProcessCode);
                //        var data = await repository.ExecuteReturnFirsOrDefault<Process>(proc, param);
                //        returnData.ResponseMessage = Resource.SUCCESS_Success;
                //        returnData.Data = data;
                //        returnData.IsSuccess = true;
                //        break;
                //    default:
                //        break;
                //}
                //returnData.Data = result;
                return result;
            }
            catch (Exception)
            {
                //returnData.HttpResponseCode = 500;
                return -1;
                throw;
            }
        }

        public async Task<int> Modify(UserInfo model)
        {
            string proc = "p_UserDAO_Modify";
            var param = new DynamicParameters();
            param.Add("@id", model.ID);
            param.Add("@password", model.Password ?? "");
            param.Add("@name", model.Name);
            param.Add("@email", model.Email);
            param.Add("@phone", model.Phone);
            param.Add("@address", model.Address);
            param.Add("@LocationID", 0);
            param.Add("@active", model.Active ?? true);
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
                return result;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public async Task<int> AuthorizeRoles(AuthorizeModel model)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in model.AuthorizeIDs)
            {
                str.Append(string.Concat(item, ","));
            }

            string proc = "p_UserDAO_AuthorizeRoles";
            var param = new DynamicParameters();
            param.Add("@ID", model.ID);
            param.Add("@AuthorizeIDs", str.ToString());
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
                return result;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public async Task<ResponseModel<UserModel>> GetByUserName(string userName)
        {
            var returnData = new ResponseModel<UserModel>();
            string proc = "p_UserDAO_GetByUsername";
            var param = new DynamicParameters();
            param.Add("@username", userName);
            var data = await repository.ExecuteReturnFirsOrDefault<UserModel>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;
            }
            else
            {
                returnData.ResponseMessage = Resource.ERROR_NotFound;
            }
            returnData.IsSuccess = true;
            return returnData;
        }
    }
}
