using Library.Common;
using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library;
using Dapper;
using Newtonsoft.Json;
//using Juwon.DataTransferObjects;
using System.Web;
using System.Data.SqlClient;
using RepoDb;

namespace Juwon.Services.Implements
{
    public class AppService : IAppService
    {
        private readonly IRepository repository;
        private readonly string connectionString = DatabaseConnection.CONNECTIONSTRING;

        public AppService(IRepository IRepository)
        {
            repository = IRepository;
        }

        public Task<ResponseModel<APPInfo>> Create(APPInfo model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<int>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<APPInfo>>> GetActive()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<IList<APPInfo>>> GetAll()
        {
            var returnData = new ResponseModel<IList<APPInfo>>();
            string proc = "usp_APPInfo_GetAll";
            try
            {
                var result = await repository.ExecuteReturnList<APPInfo>(proc);
                if (result.Count > 0)
                {
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
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

        public Task<ResponseModel<APPInfo>> GetById(int qcMasterId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<APPInfo>> GetByName(string qcMasterName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<APPInfo>> Modify(APPInfo model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<APPInfo>>> SearchActive(string keyWord)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IList<APPInfo>>> SearchAll(string keyWord)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<APPInfo>> UploadApp(HttpPostedFileBase httpPostedFileBase, string data, string strFileName)
        {
            var returnData = new ResponseModel<APPInfo>();
            var appDTO = JsonConvert.DeserializeObject<APPModel>(data);
            int pos = strFileName.IndexOf(".", StringComparison.Ordinal);
            string ver = strFileName.Substring(0, pos);
            pos = ver.LastIndexOf("_") + 1;
            ver = ver.Substring(pos, ver.Length - pos);

            var appInfo = new APPInfo
            {
                ID = appDTO.ID,
                Name = strFileName,
                UrlApp = $"/APK/{strFileName}",
                VesionApp = Int32.Parse(ver),
                ReleaseNotes = ver
            };

            var list = new List<APPInfo>
            {
                appInfo
            };

            try
            {

                var result = await BulkUpdate(list);

                if (result > 0)
                {
                    string proc = $"usp_APPInfo_GetById";
                    var param = new DynamicParameters();
                    param.Add("@APPInfoId", appInfo.ID);
                    var app = await repository.ExecuteReturnFirsOrDefault<APPInfo>(proc, param);
                    returnData.ResponseMessage = Resource.SUCCESS_Modify;
                    returnData.Data = app;
                    returnData.IsSuccess = true;
                }
                return returnData;
            }
            catch (Exception)
            {
                return returnData;
                throw;
            }
        }

        private async Task<int> BulkUpdate(List<APPInfo> updateList)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                //var dataTable = repository.ConvertListToDataTable(updateList);
                var effectedRows = await connection.BulkUpdateAsync(updateList);
                if (effectedRows > 0)
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
