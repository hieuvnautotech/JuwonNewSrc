using Juwon.Controllers.Base;
using Juwon.Models;
using Juwon.Services.Interfaces;
using Library.Attributes;
using Library.Helper;
using Library.Helpers.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Juwon.Controllers.Standard.General
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class MoldController : BaseController
    {
        private readonly IMoldService moldService;

        public MoldController(IMoldService IMoldService)
        {
            moldService = IMoldService;
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
            return await Task.Run(() => View("~/Views/Standard/Information/Mold/Mold.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchActive(string keyWord = "")
        {
            var result = await moldService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await moldService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MOLD_CREATE)]
        public async Task<ActionResult> CreateMold(Mold model = null)
        {
           
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var dir = "~/Img/Standard/Information/Mold/";
                bool exists = Directory.Exists(Server.MapPath(dir));
                if (!exists)
                {
                    Directory.CreateDirectory(Server.MapPath(dir));
                }
                var FileUpload = System.Web.HttpContext.Current.Request.Files["file"];
                if (FileUpload.ContentLength > 0)
                {
                    List<string> ImageExtensions = new List<string> { "IMAGE/JPG", "IMAGE/JPE", "IMAGE/JPEG", "IMAGE/BMP", "IMAGE/GIF", "IMAGE/PNG", "APPLICATION/PDF" };
                    if (ImageExtensions.Contains(FileUpload.ContentType.ToUpper()))
                    {
                        var profileName = Path.GetFileName(FileUpload.FileName);
                     

                        var comPath = Server.MapPath(dir) + profileName;

                        FileUpload.SaveAs(comPath);
                        model.Image = profileName;
                    }

                }

            }
            else
            {
                model.Image = "NoData.png";
            }

            var result = await moldService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MOLD_MODIFY)]
        public async Task<ActionResult> ModifyMold(Mold model = null)
        {
            
            var oldFileName = model.Image;
            var dir = "~/Img/Standard/Information/Mold/";
            bool exists = Directory.Exists(Server.MapPath(dir));
            if (!exists)
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var FileUpload = System.Web.HttpContext.Current.Request.Files["file"];
                if (FileUpload.ContentLength > 0)
                {
                    List<string> ImageExtensions = new List<string> { "IMAGE/JPG", "IMAGE/JPE", "IMAGE/JPEG", "IMAGE/BMP", "IMAGE/GIF", "IMAGE/PNG", "APPLICATION/PDF" };
                    if (ImageExtensions.Contains(FileUpload.ContentType.ToUpper()))
                    {
                        var profileName = Path.GetFileName(FileUpload.FileName);
                        //var ext = Path.GetExtension(FileUpload.FileName);

                        var comPath = Server.MapPath(dir) + profileName;
                        if (profileName != "NoData.png")
                        {
                            FileUpload.SaveAs(comPath);
                        }
                        model.Image = profileName;
                    }
                }

            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.Image))
                {
                    model.Image = "NoData.png";
                }

            }
            var result = await moldService.Modify(model);
            if (result.Data != null && result.Data.Image != oldFileName && oldFileName != "NoData.png")
            {
                try
                {
                    string deletePath = Request.MapPath(dir + oldFileName);
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(deletePath))
                    {
                        // If file found, delete it    
                        System.IO.File.Delete(deletePath);
                    }
                }
                catch (IOException)
                {
                    result.ResponseMessage = "Cannot delete old file !";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MOLD_DELETE)]
        public async Task<ActionResult> DeleteMold(int moldId = 0)
        {
            var result = await moldService.Delete(moldId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MOLD_DELETE)]
        public async Task<ActionResult> ActiveMold(int moldId = 0)
        {
            var result = await moldService.Active(moldId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}