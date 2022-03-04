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
    public class LocationCategoryController : BaseController
    {
        private readonly ILocationCategoryService locationCategoryService;

        public LocationCategoryController(ILocationCategoryService ILocationCategoryService)
        {
            locationCategoryService = ILocationCategoryService;
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
            return await Task.Run(() => View("~/Views/Standard/General/LocationCategory/LocationCategory.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchActive(string keyWord = "")
        {
            var result = await locationCategoryService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await locationCategoryService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.LOCATIONTYPE_CREATE)]
        public async Task<ActionResult> CreateLocationCategory(LocationCategory model = null)
        {
            var result = await locationCategoryService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.LOCATIONTYPE_MODIFY)]
        public async Task<ActionResult> ModifyLocationCategory(LocationCategory model = null)
        {
            var result = await locationCategoryService.Modify(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.LOCATIONTYPE_DELETE)]
        public async Task<ActionResult> DeleteLocationCategory(int locationCategoryId = 0)
        {
            var result = await locationCategoryService.Delete(locationCategoryId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.LOCATIONTYPE_DELETE)]
        public async Task<ActionResult> ActiveLocationCategory(int locationCategoryId = 0)
        {
            var result = await locationCategoryService.Active(locationCategoryId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}