using Dapper;
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
    public class MenuService : IMenuService
    {
        private readonly IRepository repository;

        public MenuService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<int> Create(MenuModel model)
        {
            if (string.IsNullOrEmpty(model.SecondaryMenu))
            {
                model.SecondaryMenu = "";
            }
            if (string.IsNullOrEmpty(model.TertiaryMenu))
            {
                model.TertiaryMenu = "";
            }
            if (model.MenuLevel3Orderly == null)
            {
                model.MenuLevel3Orderly = 0;
            }
            if (string.IsNullOrEmpty(model.Link))
            {
                model.Link = "";
            }

            string proc = "p_MenuDAO_Create";
            var param = new DynamicParameters();
            param.Add("@menuCategory", model.MenuCategory);
            param.Add("@primary", model.PrimaryMenu);
            param.Add("@secondary", model.SecondaryMenu);
            param.Add("@tertiary", model.TertiaryMenu);
            param.Add("@menuLevel", model.MenuLevel);
            param.Add("@link", model.Link);

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

        public async Task<ResponseModel<IList<MenuModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<MenuModel>>();
            string proc = "p_MenuDAO_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<MenuModel>(proc);
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

        public async Task<ResponseModel<IList<MenuModel>>> GetAllExceptRoot()
        {
            var returnData = new ResponseModel<IList<MenuModel>>();
            string proc = "p_MenuDAO_GetAllExceptRoot";
            try
            {
                var result = await repository.ExecuteReturnList<MenuModel>(proc);
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

        public async Task<ResponseModel<MenuModel>> GetByPrimarySecondary(string primary, string secondary, string tertiary)
        {
            var returnData = new ResponseModel<MenuModel>();
            string proc = "p_MenuDAO_GetByFullName";
            var param = new DynamicParameters();
            param.Add("@primary", primary);
            param.Add("@secondary", secondary);
            param.Add("@tertiary", tertiary);
            var data = await repository.ExecuteReturnFirsOrDefault<MenuModel>(proc, param);
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

        public async Task<IList<int>> GetMenuIDByRoleID(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            string proc = "p_MenuDAO_GetMenuIDByRoleID";
            var param = new DynamicParameters();
            param.Add("@id", id);
            var result = await repository.ExecuteReturnList<int>(proc, param);
            return result;
        }

        public async Task<IList<int>> GetPermissionIDByRoleID(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            string proc = "p_MenuDAO_GetPermissionIDByRoleID";
            var param = new DynamicParameters();
            param.Add("@id", id);
            var result = await repository.ExecuteReturnList<int>(proc, param);
            return result;
        }

        public async Task<int> Modify(MenuModel model)
        {
            if (string.IsNullOrEmpty(model.SecondaryMenu))
            {
                model.SecondaryMenu = "";
            }
            if (string.IsNullOrEmpty(model.TertiaryMenu))
            {
                model.TertiaryMenu = "";
            }
            if (model.MenuLevel3Orderly == null)
            {
                model.MenuLevel3Orderly = 0;
            }
            if (string.IsNullOrEmpty(model.Link))
            {
                model.Link = "";
            }

            string proc = "p_MenuDAO_Modify";
            var param = new DynamicParameters();
            param.Add("@Id", model.ID);
            param.Add("@menuCategory", model.MenuCategory);
            param.Add("@primary", model.PrimaryMenu);
            param.Add("@primaryOrder", model.MenuOrderly);
            param.Add("@secondary", model.SecondaryMenu);
            param.Add("@secondaryOrder", model.MenuLevel2Orderly);
            param.Add("@tertiary", model.TertiaryMenu);
            param.Add("@tertiaryOrder", model.MenuLevel3Orderly);
            param.Add("@menuLevel", model.MenuLevel);
            param.Add("@link", model.Link);
            param.Add("@active", model.Active);

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

        public async Task<ResponseModel<IList<MenuModel>>> Search(string keyWord)
        {
            var returnData = new ResponseModel<IList<MenuModel>>();
            string proc = "p_MenuDAO_Search";
            var param = new DynamicParameters();
            param.Add("@keyWord", keyWord);
            var data = await repository.ExecuteReturnList<MenuModel>(proc, param);
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
    }
}