using Juwon.Models;
using Library.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface IUserInfoService
    {
        Task<ResponseModel<IList<UserModel>>> GetAll();
        Task<UserModel> GetUserByUserName(string userName);
        Task<ResponseModel<UserModel>> GetByUserName(string userName);
        Task<ResponseModel<UserModel>> GetByID(int id);
        Task<ResponseModel<IList<UserModel>>> Search(string keyWord);
        Task<int> RecordUserLog(int userInfoId, string ipAddress, string sessionId);
        Task<IList<string>> GetUserRolesByUserName(string userName);
        Task<IList<string>> GetUserPermissionsByUserName(string userName);
        Task<IList<MenuModel>> GetUserMenusByUserName(string userName);
        Task<int> ChangePassword(string userName, string password);
        Task<int> Create(UserInfo model);
        Task<int> Modify(UserInfo model);
        Task<int> AuthorizeRoles(AuthorizeModel model);
    }
}
