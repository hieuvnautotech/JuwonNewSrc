using Library.Common;
using Juwon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface ICommonMasterService
    {
        Task<ResponseModel<IList<CommonMaster>>> GetAll();
        Task<ResponseModel<IList<CommonMaster>>> GetAllActive();
        Task<ResponseModel<CommonMaster>> GetByID(int Id);
        Task<ResponseModel<CommonMaster>> GetByCode(string code);
        Task<ResponseModel<IList<CommonMaster>>> Search(string keyWord);
        Task<ResponseModel<CommonMaster>> Create(CommonMaster model);
        Task<ResponseModel<CommonMaster>> Modify(CommonMaster model);
        Task<ResponseModel<CommonMaster>> DeleteByID(int ID);
    }
}
