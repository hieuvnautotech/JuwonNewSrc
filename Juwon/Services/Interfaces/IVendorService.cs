using Library.Common;
using Juwon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Models.DTOs;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IVendorService : IBase<VendorModel>
    {
        Task<ResponseModel<VendorModel>> GetByCode(string vendorCode);
        Task<ResponseModel<int>> Active(int vendorId);
    }
}
