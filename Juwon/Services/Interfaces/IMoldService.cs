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
    public interface IMoldService : IBase<Mold>
    {
        Task<ResponseModel<int>> Active(int moldId);
    }
}
