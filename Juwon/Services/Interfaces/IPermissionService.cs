using Juwon.Models;
using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<ResponseModel<IList<PermissionModel>>> GetAll();
        Task<ResponseModel<IList<PermissionModel>>> GetAllExceptRoot();
        Task<ResponseModel<PermissionModel>> GetByID(int Id);
        Task<ResponseModel<PermissionModel>> GetByName(string name);
        Task<ResponseModel<IList<PermissionModel>>> Search(string keyWord);
        Task<ResponseModel<Permission>> Create(Permission model);
        Task<ResponseModel<Permission>> Modify(Permission model);
        Task<ResponseModel<Permission>> DeleteByID(int ID);
    }
}
