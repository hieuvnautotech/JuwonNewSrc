using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface IMenuService
    {
        Task<ResponseModel<IList<MenuModel>>> GetAll();
        Task<ResponseModel<IList<MenuModel>>> GetAllExceptRoot();
        Task<ResponseModel<MenuModel>> GetByPrimarySecondary(string primary, string secondary, string tertiary);
        Task<ResponseModel<IList<MenuModel>>> Search(string keyWord);
        Task<int> Create(MenuModel model);
        Task<int> Modify(MenuModel model);
        Task<IList<int>> GetMenuIDByRoleID(int? id);
        Task<IList<int>> GetPermissionIDByRoleID(int? id);
    }
}
