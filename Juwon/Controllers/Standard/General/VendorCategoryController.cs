
using Library.Attributes;
using Juwon.Controllers.Base;
using Library.Helpers.Authentication;
using Juwon.Models;
using Juwon.Services.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Library.Helper;

namespace Juwon.Controllers.Standard.General
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class VendorCategoryController : BaseController
    {
        private readonly IVendorCategoryService vendorCategoryService;

        public VendorCategoryController(IVendorCategoryService IVendorCategoryService)
        {
            vendorCategoryService = IVendorCategoryService;
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
            return await Task.Run(() => View("~/Views/Standard/General/VendorCategory/VendorCategory.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await vendorCategoryService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await vendorCategoryService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.VENDORCATEGORY_CREATE)]
        public async Task<ActionResult> CreateVendorCategory(VendorCategory obj = null)
        {
            obj.CreatedBy = SessionHelper.GetUserSession().ID;
            var result = await vendorCategoryService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.VENDORCATEGORY_MODIFY)]
        public async Task<ActionResult> ModifyVendorCategory(VendorCategory obj = null)
        {
            obj.ModifiedBy = SessionHelper.GetUserSession().ID;
            var result = await vendorCategoryService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.VENDORCATEGORY_DELETE)]
        public async Task<ActionResult> DeleteVendorCategory(int vendorCategoryId = 0)
        {
            var result = await vendorCategoryService.Delete(vendorCategoryId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.VENDORCATEGORY_DELETE)]
        public async Task<ActionResult> ActiveVendorCategory(int vendorCategoryId = 0)
        {
            var result = await vendorCategoryService.Active(vendorCategoryId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetAllActive()
        {
            var result = await vendorCategoryService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}