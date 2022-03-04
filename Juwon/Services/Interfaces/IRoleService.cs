using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseModel<IList<RoleModel>>> GetAll();
        Task<ResponseModel<RoleModel>> GetByName(string name);
        Task<ResponseModel<IList<RoleModel>>> Search(string keyWord);
        Task<int> Create(RoleModel model);
        Task<int> AuthorizeMenus(RoleMenuModel model);
        Task<int> AuthorizePermissions(RoleMenuModel model);
        Task<IList<int>> GetRoleIDByUserID(int? id);
    }
}
