using Juwon.Models;
using Juwon.Services.Base;
using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface ICommonDetailService : IBase<CommonDetailModel>
    {
        Task<ResponseModel<IList<CommonDetailModel>>> GetAllByMasterCode(string masterCode);
        Task<ResponseModel<CommonDetailModel>> GetByCodeAndMasterCode(string code, string masterCode);
        Task<ResponseModel<IList<CommonDetailModel>>> Search(string keyWord, string MasterCode);
    }
}
