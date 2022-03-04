using Juwon.Controllers.Base;
using Juwon.Models.DTOs;
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

namespace Juwon.Controllers.Standard.Information
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class AreaController : BaseController
    {
        private readonly IAreaService areaService;
        private readonly IAreaCategoryService areaCategoryService;

        public AreaController(IAreaService IAreaService, IAreaCategoryService IAreaCategoryService)
        {
            areaService = IAreaService;
            areaCategoryService = IAreaCategoryService;
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
            return await Task.Run(() => View("~/Views/Standard/Information/Area/Area.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchActive(string keyWord = "")
        {
            var result = await areaService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await areaService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREA_CREATE)]
        public async Task<ActionResult> CreateArea(AreaModel obj = null)
        {
            var result = await areaService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREA_MODIFY)]
        public async Task<ActionResult> ModifyArea(AreaModel obj = null)
        {
            var result = await areaService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREA_DELETE)]
        public async Task<ActionResult> DeleteArea(int areaId = 0)
        {
            var result = await areaService.Delete(areaId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREA_DELETE)]
        public async Task<ActionResult> ActiveArea(int areaId = 0)
        {
            var result = await areaService.Active(areaId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetAreaCategories()
        {
            var result = await areaCategoryService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}