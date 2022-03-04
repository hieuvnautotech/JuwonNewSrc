using Dapper;
using Library.Common;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Juwon.Services.Implements
{
    public class LoginService : ILoginService
    {
        private readonly IRepository repository;
        public LoginService(IRepository iRepository)
        {
            repository = iRepository;
        }

        public async Task<int> CheckLogin(LoginModel model)
        {

            string proc = "p_UserDAO_CheckLogin";
            var param = new DynamicParameters();
            param.Add("@UserName", model.UserName);
            param.Add("@Password", MD5Encryptor.MD5Hash(model.Password));

            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);

                return result;
            }
            catch (Exception)
            {
                return -3;
                throw;
            }
        }

        public async Task<int> Logout(int userInfoId, string ipAddress, string sessionId)
        {
            string proc = "usp_UserInfo_Logout";
            var param = new DynamicParameters();
            param.Add("@UserInfoId", userInfoId);
            param.Add("@IpAddress", ipAddress);
            param.Add("@SessionId", sessionId);

            try
            {
                var result = await repository.ExecuteReturnScalar<int>(proc, param);

                return result;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
    }
}
