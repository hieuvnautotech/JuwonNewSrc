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
    public interface IQCDetailService : IBase<QCDetail>
    {
        Task<ResponseModel<IList<QCDetail>>> GetAllByQCMasterId(int qcMasterId);
        Task<ResponseModel<IList<QCDetail>>> GetActiveByQCMasterId(int qcMasterId);
        Task<ResponseModel<int>> Active(int qcDetailId);
    }
}
