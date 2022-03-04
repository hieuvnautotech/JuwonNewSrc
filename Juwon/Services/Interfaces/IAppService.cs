using Library.Common;
using Juwon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Juwon.Services.Base;

namespace Juwon.Services.Interfaces
{
    public interface IAppService : IBase<APPInfo>
    {
        Task<ResponseModel<APPInfo>> UploadApp(HttpPostedFileBase httpPostedFileBase, string data, string strFileName);
    }
}
