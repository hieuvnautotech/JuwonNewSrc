using Juwon.Models;
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
    public interface ILocationService : IBase<LocationModel>
    {
        Task<ResponseModel<int>> Active(int locationId);
    }
}
