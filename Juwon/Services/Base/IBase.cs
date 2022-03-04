using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juwon.Services.Base
{
    public interface IBase<T>
    {
        Task<ResponseModel<IList<T>>> GetAll();
        Task<ResponseModel<IList<T>>> GetActive();
        Task<ResponseModel<T>> GetById(int id);
        Task<ResponseModel<T>> GetByName(string name);
        Task<ResponseModel<IList<T>>> SearchAll(string keyWord);
        Task<ResponseModel<IList<T>>> SearchActive(string keyWord);
        Task<ResponseModel<T>> Create(T model);
        Task<ResponseModel<T>> Modify(T model);
        Task<ResponseModel<int>> Delete(int id);
    }
}
