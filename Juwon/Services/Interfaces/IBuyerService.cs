using Library.Common;
using Juwon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IBuyerService : IBase<Buyer>
    {
        Task<ResponseModel<Buyer>> GetByCode(string buyerCode);
        Task<ResponseModel<IList<Buyer>>> GetByVendorId(int vendorId);
        Task<ResponseModel<IList<Buyer>>> GetAllByVendorId(int vendorId);
        Task<ResponseModel<int>> Active(int buyerId);
    }
}
