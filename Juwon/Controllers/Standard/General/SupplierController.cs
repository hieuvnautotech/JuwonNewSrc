
using Library.Attributes;
using Juwon.Controllers.Base;
using Library.Helpers.Authentication;
using Juwon.Services.Interfaces;
using System;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using Juwon.Models;
using Library.Helper;

namespace Juwon.Controllers.Standard.General
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class SupplierController : BaseController
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService ISupplierService)
        {
            supplierService = ISupplierService;
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
            return await Task.Run(() => View("~/Views/Standard/General/Supplier/Supplier.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            try
            {
                var result = await supplierService.SearchActive(keyWord);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await supplierService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.SUPPLIER_CREATE)]
        public async Task<ActionResult> CreateSupplier(Supplier obj = null)
        {
            var result = await supplierService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.SUPPLIER_MODIFY)]
        public async Task<ActionResult> ModifySupplier(Supplier obj = null)
        {
            var result = await supplierService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.SUPPLIER_DELETE)]
        public async Task<ActionResult> DeleteSupplier(int supplierId = 0)
        {
            var result = await supplierService.Delete(supplierId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.SUPPLIER_DELETE)]
        public async Task<ActionResult> ActiveSupplier(int supplierId = 0)
        {
            var result = await supplierService.Active(supplierId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}