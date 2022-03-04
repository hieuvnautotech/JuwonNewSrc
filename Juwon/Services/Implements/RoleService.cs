using Dapper;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using Library;
using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Juwon.Services.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IRepository repository;

        public RoleService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<int> AuthorizeMenus(RoleMenuModel model)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in model.MenuIDs)
            {
                str.Append(string.Concat(item, ","));
            }

            string proc = "p_RoleDAO_AuthorizeMenus";
            var param = new DynamicParameters();
            param.Add("@roleID", model.RoleID);
            param.Add("@menuIDs", str.ToString());
            return await repository.ExecuteReturnScalar<int>(proc, param);
        }

        public async Task<int> AuthorizePermissions(RoleMenuModel model)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in model.PermissionIDs)
            {
                str.Append(string.Concat(item, ","));
            }

            string proc = "p_RoleDAO_AuthorizePermissions";
            var param = new DynamicParameters();
            param.Add("@roleID", model.RoleID);
            param.Add("@permissionIDs", str.ToString());
            return await repository.ExecuteReturnScalar<int>(proc, param);
        }

        public async Task<int> Create(RoleModel model)
        {
            string proc = "p_RoleDAO_Create";
            var param = new DynamicParameters();
            param.Add("@name", model.Name.ToUpper());
            param.Add("@roleCategory", model.RoleCategory);
            param.Add("@description", model.Description);
           
            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);
                return result;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public async Task<ResponseModel<IList<RoleModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<RoleModel>>();
            string proc = "p_RoleDAO_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<RoleModel>(proc);
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

        public async Task<ResponseModel<RoleModel>> GetByName(string name)
        {
            var returnData = new ResponseModel<RoleModel>();
            string proc = "p_RoleDAO_GetByName";
            var param = new DynamicParameters();
            param.Add("@name", name);
            var data = await repository.ExecuteReturnFirsOrDefault<RoleModel>(proc, param);
            if (data != null)
            {
                returnData.ResponseMessage = Resource.SUCCESS_Success;
                returnData.Data = data;
                returnData.IsSuccess = true;
            }
            else
            {
                returnData.ResponseMessage = Resource.ERROR_NotFound;
            }
            return returnData;
        }

        public async Task<IList<int>> GetRoleIDByUserID(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            try
            {
                string proc = "p_RoleDAO_GetRoleIDByRoleID";
                var param = new DynamicParameters();
                param.Add("@id", id);
                var result = await repository.ExecuteReturnList<int>(proc, param);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel<IList<RoleModel>>> Search(string keyWord)
        {
            var returnData = new ResponseModel<IList<RoleModel>>();
            string proc = "p_RoleDAO_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            try
            {
                var result = await repository.ExecuteReturnList<RoleModel>(proc, param);
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
    }
}