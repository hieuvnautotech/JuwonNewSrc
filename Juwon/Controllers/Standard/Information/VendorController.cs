
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
using Juwon.Models.DTOs;

namespace Juwon.Controllers.Standard.Information
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class VendorController : BaseController
    {
        private readonly IVendorService vendorService;
        private readonly IVendorCategoryService vendorCategoryService;
        private readonly IDestinationService destinationService;
        private readonly IBuyerService buyerService;

        public VendorController(IVendorService IVendorService, IVendorCategoryService IVendorCategoryService, IDestinationService IDestinationService, IBuyerService IBuyerService)
        {
            vendorService = IVendorService;
            vendorCategoryService = IVendorCategoryService;
            destinationService = IDestinationService;
            buyerService = IBuyerService;
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
            return await Task.Run(() => View("~/Views/Standard/Information/Vendor/Vendor.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await vendorService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await vendorService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.VENDOR_CREATE)]
        public async Task<ActionResult> CreateVendor(VendorModel obj = null)
        {
            var result = await vendorService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.VENDOR_MODIFY)]
        public async Task<ActionResult> ModifyVendor(VendorModel obj = null)
        {
            var result = await vendorService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.VENDOR_DELETE)]
        public async Task<ActionResult> DeleteVendor(int vendorId = 0)
        {
            var result = await vendorService.Delete(vendorId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.VENDOR_DELETE)]
        public async Task<ActionResult> ActiveVendor(int vendorId = 0)
        {
            var result = await vendorService.Active(vendorId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetVendorCategory()
        {
            var result = await vendorCategoryService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetDestination()
        {
            var result = await destinationService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetBuyers()
        {
            var result = await buyerService.SearchActive("");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetBuyersByVendorId(int vendorId = 0)
        {
            var result = await buyerService.GetByVendorId(vendorId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}