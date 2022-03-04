using Library.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Models;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IVendorCategoryService : IBase<VendorCategory>
    {
        Task<ResponseModel<int>> Active(int vendorCategoryId);
    }
}
