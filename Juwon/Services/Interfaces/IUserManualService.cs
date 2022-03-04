using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface IUserManualService
    {
        Task<ResponseModel<IList<UserManualModel>>> GetAll();
        Task<ResponseModel<UserManualModel>> GetByID(int? id);
        //Task<ResponseModel<IList<UserManualModel>>> Search(string languageCode, string menuCode, string menuName);
        Task<ResponseModel<IList<UserManualModel>>> Search(string languageCode, string menuCode);
        Task<ResponseModel<IList<UserManualModel>>> Search2(string languageCode, string menuCode, string menuName);
        Task<int> DeleteByID(int ID);
        Task<int> Insert(string name, string content, string menuCode, string menuLevel, string languageCode, string description);
        Task<int> Update(string Id, string name, string content, string menuCode, string menuLevel, string languageCode, string description);
    }
}
