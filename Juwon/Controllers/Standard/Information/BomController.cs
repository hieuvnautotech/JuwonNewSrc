using Juwon.Models;
using Juwon.Models.DTOs;
using Juwon.Services.Interfaces;
using Library.Attributes;
using Library.Helpers.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Juwon.Controllers.Standard.Information
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]

    public class BomController : Controller
    {
        private readonly IBomService bomService;
        private readonly IBomDetailService bomDetailService;
        private readonly IMoldService moldService;
        private readonly IPartService partService;
        private readonly IMaterialService materialService;

        public BomController(IBomService IBomService, IBomDetailService IBomDetailService, IMoldService IMoldService, IPartService IPartService, IMaterialService IMaterialService)
        {
            bomService = IBomService;
            bomDetailService = IBomDetailService;
            moldService = IMoldService;
            partService = IPartService;
            materialService = IMaterialService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View("~/Views/Standard/Information/Bom/Bom.cshtml"));
        }

        #region Bom
        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await bomService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            try
            {
                var result = await bomService.SearchActive(keyWord);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetMold()
        {
            try
            {
                var result = await moldService.GetActive();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BOM_CREATE)]
        public async Task<ActionResult> CreateBom(BomModel model = null)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var dir = "~/Img/Standard/Information/Bom/";
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
                        //var ext = Path.GetExtension(FileUpload.FileName);

                        var comPath = Server.MapPath(dir) + profileName;

                        FileUpload.SaveAs(comPath);
                        model.FileUpload = profileName;
                    }

                }

            }
            else
            {
                model.FileUpload = "NoData.png";
            }

            var result = await bomService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BOM_MODIFY)]
        public async Task<ActionResult> ModifyBom(BomModel model = null)
        {
            var oldFileName = model.FileUpload;
            var dir = "~/Img/Standard/Information/Bom/";
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
                        model.FileUpload = profileName;
                    }
                }

            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.FileUpload))
                {
                    model.FileUpload = "NoData.png";
                }

            }
            var result = await bomService.Modify(model);
            if (result.Data != null && result.Data.FileUpload != oldFileName && oldFileName != "NoData.png")
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
        [Permission(PermissionConstants.BOM_DELETE)]
        public async Task<ActionResult> DeleteBom(int bomId = 0)
        {
            var result = await bomService.Delete(bomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BOM_DELETE)]
        public async Task<ActionResult> ActiveBom(int BomId = 0)
        {
            var result = await bomService.Active(BomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region BomDetail
        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetPart()
        {
            var result = await partService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetMaterial()
        {
            var result = await materialService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SearchActiveByBomId(int bomId = 0, string keyWord = "")
        {
            var result = await bomDetailService.SearchActiveByBomId(bomId, keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BOMDETAIL_CREATE)]
        public async Task<ActionResult> CreateBomDetail(BomDetailModel model = null)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var dir = "~/Img/Standard/Information/BomDetail/";
                bool exists = Directory.Exists(Server.MapPath(dir));
                if (!exists)
                {
                    Directory.CreateDirectory(Server.MapPath(dir));
                }
                var FileUpload = System.Web.HttpContext.Current.Request.Files["File"];
                if (FileUpload.ContentLength > 0)
                {
                    List<string> ImageExtensions = new List<string> { "IMAGE/JPG", "IMAGE/JPE", "IMAGE/JPEG", "IMAGE/BMP", "IMAGE/GIF", "IMAGE/PNG", "APPLICATION/PDF" };
                    if (ImageExtensions.Contains(FileUpload.ContentType.ToUpper()))
                    {
                        var profileName = Path.GetFileName(FileUpload.FileName);
                        //var ext = Path.GetExtension(FileUpload.FileName);

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

            var result = await bomDetailService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
