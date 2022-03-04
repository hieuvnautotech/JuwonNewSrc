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
    public interface IMaterialService : IBase<MaterialModel>
    {
        Task<ResponseModel<MaterialModel>> GetByCode(string materialCode);
        Task<ResponseModel<int>> Active(int materialId);
    }
}
