using Dapper;
using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using Library;
using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Juwon.Services.Implements
{
    public class UserManualService : IUserManualService
    {
        private readonly IRepository repository;

        public UserManualService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public async Task<int> DeleteByID(int ID)
        {
            string proc = "p_UserManualDAO_DelByID";
            var param = new DynamicParameters();
            param.Add("@ID", ID);

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

        public async Task<ResponseModel<IList<UserManualModel>>> GetAll()
        {
            var returnData = new ResponseModel<IList<UserManualModel>>();
            string proc = "p_UserManualDAO_GetALL";
            try
            {
                var result = await repository.ExecuteReturnList<UserManualModel>(proc);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                }
                else
                {
                    returnData.Data = null;
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<UserManualModel>> GetByID(int? id)
        {
            var returnData = new ResponseModel<UserManualModel>();
            string proc = "p_UserManualDAO_GetById";
            var param = new DynamicParameters();
            param.Add("@id", id);
            try
            {
                var result = await repository.ExecuteReturnFirsOrDefault<UserManualModel>(proc, param);
                if (result != null)
                {
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                    returnData.Data = result;
                    returnData.IsSuccess = true;
                }
                else
                {
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<int> Insert(string name, string content, string menuCode, string menuLevel, string languageCode, string description)
        {
            Dictionary<string, string> checkDic = new Dictionary<string, string>
            {
                { "name", name },
                { "content", content },
                { "menuCode", menuCode },
                { "menuLevel", menuLevel },
                { "languageCode", languageCode }
            };
            foreach (var item in checkDic)
            {
                if (item.Value == "" || item.Value == "<p><br></p>")
                {
                    return -2;
                }
            }

            string proc = $"usp_UserManual_Create";
            var param = new DynamicParameters();
            param.Add("@Name", name);
            param.Add("@Content", content);
            param.Add("@MenuCode", menuCode);
            param.Add("@MenuLevel", int.Parse(menuLevel));
            param.Add("@LanguageCode", languageCode);

            var result = await repository.ExecuteReturnScalar<int>(proc, param);
            return result;
        }

        public async Task<ResponseModel<IList<UserManualModel>>> Search(string languageCode, string menuCode)
        {
            var returnData = new ResponseModel<IList<UserManualModel>>();
            string proc = "p_UserManualDAO_Search2";
            var param = new DynamicParameters();
            param.Add("@p_languageCode", languageCode);
            param.Add("@p_menuCode", menuCode);
            try
            {
                var result = await repository.ExecuteReturnList<UserManualModel>(proc, param);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                }
                else
                {
                    returnData.Data = null;
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<ResponseModel<IList<UserManualModel>>> Search2(string languageCode, string menuCode, string menuName)
        {
            var returnData = new ResponseModel<IList<UserManualModel>>();
            string proc = "p_UserManualDAO_Search";
            var param = new DynamicParameters();
            param.Add("@p_languageCode", languageCode);
            param.Add("@p_menuCode", menuCode);
            param.Add("@p_menuName", menuName);
            try
            {
                var result = await repository.ExecuteReturnList<UserManualModel>(proc, param);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                }
                else
                {
                    returnData.Data = null;
                    returnData.ResponseMessage = Resource.ERROR_NotFound;
                }

                returnData.IsSuccess = true;
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
            }
        }

        public async Task<int> Update(string Id, string name, string content, string menuCode, string menuLevel, string languageCode, string description)
        {
            if (string.IsNullOrEmpty(Id) || !int.TryParse(Id, out int number))
            {
                return -3;
            }

            Dictionary<string, string> checkDic = new Dictionary<string, string>();
            checkDic.Add("name", name);
            checkDic.Add("content", content);
            checkDic.Add("menuCode", menuCode);
            checkDic.Add("menuLevel", menuLevel);
            checkDic.Add("languageCode", languageCode);
            foreach (var item in checkDic)
            {
                if (item.Value == "" || item.Value == "<p><br></p>")
                {
                    return -2;
                }
            }

            string proc = $"usp_UserManual_Modify";
            var param = new DynamicParameters();
            param.Add("@Id", number);
            param.Add("@Name", name);
            param.Add("@Content", content);
            param.Add("@MenuCode", menuCode);
            param.Add("@MenuLevel", int.Parse(menuLevel));
            param.Add("@LanguageCode", languageCode);

            var result = await repository.ExecuteReturnScalar<int>(proc, param);
            return result;
        }
    }
}