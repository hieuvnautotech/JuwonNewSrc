using Juwon.Models.DTOs;
using Juwon.Services.Base;
using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface IBomDetailService : IBase<BomDetailModel>
    {
        Task<ResponseModel<IList<BomDetailModel>>> GetActiveBomDetailByBomId(int bomId);
        Task<ResponseModel<IList<BomDetailModel>>> GetAllBomDetailByBomId(int bomId);
        Task<ResponseModel<IList<BomDetailModel>>> SearchActiveByBomId(int bomId, string keyWord);
        Task<ResponseModel<IList<BomDetailModel>>> SearchAllByBomId(int bomId, string keyWord);
    }
}
