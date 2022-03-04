using Juwon.Controllers.Base;
using Juwon.Models;
using Juwon.Services.Interfaces;
using Library.Attributes;
using Library.Helper;
using Library.Helpers.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Juwon.Controllers.Standard.General
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class AreaCategoryController : BaseController
    {
        private readonly IAreaCategoryService areacategoryService;

        public AreaCategoryController(IAreaCategoryService IAreaCategoryService)
        {
            areacategoryService = IAreaCategoryService;
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
            return await Task.Run(() => View("~/Views/Standard/General/AreaCategory/AreaCategory.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await areacategoryService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREACATEGORY_CREATE)]
        public async Task<ActionResult> CreateAreaCategory(AreaCategory obj = null)
        {
            var result = await areacategoryService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREACATEGORY_MODIFY)]
        public async Task<ActionResult> ModifyAreaCategory(AreaCategory obj = null)
        {
            var result = await areacategoryService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREACATEGORY_DELETE)]
        public async Task<ActionResult> DeleteAreaCategory(int areaCategoryid = 0)
        {
            var result = await areacategoryService.Delete(areaCategoryid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREACATEGORY_DELETE)]
        public async Task<ActionResult> ActiveAreaCategory(int areaCategoryid = 0)
        {
            var result = await areacategoryService.Active(areaCategoryid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await areacategoryService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}