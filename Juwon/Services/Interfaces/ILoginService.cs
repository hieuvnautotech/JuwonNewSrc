using Library.Common;
using System.Threading.Tasks;

namespace Juwon.Services.Interfaces
{
    public interface ILoginService
    {
        Task<int> CheckLogin(LoginModel model);
        Task<int> Logout(int userInfoId, string ipAddress, string sessionId);
    }
}
