using Library.Common;
using Juwon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IHieuService : IBase<Supplier>
    {
        Task<ResponseModel<int>> Active(int supplierId);
    }
}
