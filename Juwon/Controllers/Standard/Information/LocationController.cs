using Juwon.Controllers.Base;
using Juwon.Models;
using Juwon.Models.DTOs;
using Juwon.Services.Interfaces;
using Library.Attributes;
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
    public class LocationController : BaseController
    {
        private readonly ILocationService locationService;
        private readonly ILocationCategoryService locationCategoryService;
        private readonly IAreaService areaService;

        public LocationController(ILocationService ILocationService, ILocationCategoryService ILocationCategoryService, IAreaService IAreaService)
        {
            locationService = ILocationService;
            locationCategoryService = ILocationCategoryService;
            areaService = IAreaService;
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
            return await Task.Run(() => View("~/Views/Standard/Information/Location/Location.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchActive(string keyWord = "")
        {
            var result = await locationService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await locationService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.LOCATION_CREATE)]
        public async Task<ActionResult> CreateLocation(LocationModel model = null)
        {
            var result = await locationService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.LOCATION_MODIFY)]
        public async Task<ActionResult> ModifyLocation(LocationModel model = null)
        {
            var result = await locationService.Modify(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.LOCATION_DELETE)]
        public async Task<ActionResult> DeleteLocation(int LocationId = 0)
        {
            var result = await locationService.Delete(LocationId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.LOCATION_DELETE)]
        public async Task<ActionResult> ActiveLocation(int LocationId = 0)
        {
            var result = await locationService.Active(LocationId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetLocationCategories()
        {
            var result = await locationCategoryService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetAreas()
        {
            var result = await areaService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}