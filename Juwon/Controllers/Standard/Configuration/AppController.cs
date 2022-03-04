using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Library.Attributes;
using Library.Helpers.Authentication;
using Juwon.Models;
//using Juwon.DataTransferObjects;
using Library;
using Juwon.Controllers.Base;
using Library.Common;
using Juwon.Services.Interfaces;
using System.Threading.Tasks;

namespace Juwon.Controllers.Standard.Configuration
{
    [Role(RoleConstants.ROOT)]
    public class AppController : BaseController
    {
        private readonly IAppService appService;

        public AppController(IAppService IAppService)
        {
            appService = IAppService;
        }

        protected override JsonResult Json(object data, string contentType,
        Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //return View("~/Views/Standard/Configuration/App/App.cshtml");
            return await Task.Run(() => View("~/Views/Standard/Configuration/App/App.cshtml"));
        }

        [HttpGet]
        public async Task<ActionResult> Grid()
        {
            //using (var _db = new _DbContext())
            //{
            //    var appList = _db.APPInfo.ToList();
            //    return Json(appList, JsonRequestBehavior.AllowGet);
            //}
            var result = await appService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UploadApp(HttpPostedFileBase httpPostedFileBase, string data)
        {
            string strFileName;
            var returnData = new ResponseModel<APPModel>();
            if (Request.Files.Count > 0)
            {
                strFileName = httpPostedFileBase.FileName;
                string strFilePath = Path.Combine(Server.MapPath("~/APK/"), Path.GetFileName(strFileName)); ;
                httpPostedFileBase.SaveAs(strFilePath);
            }
            else
            {
                returnData.ResponseMessage = Resource.WARN_NoImportedFile;
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }

            var result = await appService.UploadApp(httpPostedFileBase, data, strFileName);
            return Json(result, JsonRequestBehavior.AllowGet);


            //var appInfo = JsonConvert.DeserializeObject<APPDTO>(data);

            //using (var _db = new _DbContext())
            //{
            //    var app = _db.APPInfo.Where(x => x.ID == appInfo.ID).FirstOrDefault();
            //    app.Name = strFileName;
            //    app.UrlApp = $"/APK/{strFileName}";
            //    int pos = strFileName.IndexOf(".", StringComparison.Ordinal);
            //    string ver = strFileName.Substring(0, pos);
            //    pos = ver.LastIndexOf("_") + 1;
            //    ver = ver.Substring(pos, ver.Length - pos);
            //    app.VesionApp = Int32.Parse(ver);
            //    app.ReleaseNotes = ver;


            //    try
            //    {
            //        _db.SaveChanges();
            //        return Json(new { flag = true, message = Resource.SUCCESS_Success }, JsonRequestBehavior.AllowGet);
            //    }
            //    catch (Exception)
            //    {
            //        return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
            //    }
            //}
        }
    }
}