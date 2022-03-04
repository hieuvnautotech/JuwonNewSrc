using Library.Common;
using Juwon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IDestinationService : IBase<Destination>
    {
        Task<ResponseModel<Destination>> GetByCode(string destinationCode);
        Task<ResponseModel<int>> Active(int destinationId);
    }
}
