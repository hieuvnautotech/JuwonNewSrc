
using Library.Attributes;
using Juwon.Controllers.Base;
using Library.Helpers.Authentication;
using Juwon.Services.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Juwon.Models;
using Library.Helper;

namespace Juwon.Controllers.Standard.Information
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class ColorController : BaseController
    {
        private readonly IColorService colorService;

        public ColorController(IColorService IColorService)
        {
            colorService = IColorService;
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
            //return View("~/Views/Standard/Information/Color/Color.cshtml");
            return await Task.Run(() => View("~/Views/Standard/Information/Color/Color.cshtml"));
        }


        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await colorService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await colorService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BASECOLOR_CREATE)]
        public async Task<ActionResult> CreateColor(Color obj = null)
        {
            obj.CreatedBy = SessionHelper.GetUserSession().ID;
            var result = await colorService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BASECOLOR_MODIFY)]
        public async Task<ActionResult> ModifyColor(Color obj = null)
        {
            obj.ModifiedBy = SessionHelper.GetUserSession().ID;
            var result = await colorService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BASECOLOR_DELETE)]
        public async Task<ActionResult> DeleteColor(int colorId = 0)
        {
            var result = await colorService.Delete(colorId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BASECOLOR_DELETE)]
        public async Task<ActionResult> ActiveColor(int colorId = 0)
        {
            var result = await colorService.Active(colorId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}