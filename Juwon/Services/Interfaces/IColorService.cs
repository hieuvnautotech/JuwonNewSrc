using Library.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Models;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IColorService : IBase<Color>
    {
        Task<ResponseModel<Color>> GetByCode(string colorCode);
        Task<ResponseModel<int>> Active(int colorId);
    }
}
