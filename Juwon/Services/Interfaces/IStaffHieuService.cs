using Library.Common;
using Juwon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IStaffHieuService : IBase<Staff>
    {
        Task<ResponseModel<int>> Active(int staffId);
        Task<ResponseModel<Staff>> Modify(Staff model);
    }
}
