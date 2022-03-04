using Library.Common;
using Juwon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IProcessService : IBase<Process>
    {
        Task<ResponseModel<Process>> GetByCode(string processCode);
        Task<ResponseModel<Process>> CreateTest(Process model);
        Task<int> BulkInsert(List<Process> insertList);
        Task<int> BulkUpdate(List<Process> updateList);
        Task<int> BulkMerge(List<Process> mergeList);
        Task<ResponseModel<int>> Active(int processId);
    }
}
