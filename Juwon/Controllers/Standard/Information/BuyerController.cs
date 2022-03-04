using Library.Attributes;
using Library.Helpers.Authentication;
using Juwon.Controllers.Base;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Juwon.Services.Interfaces;
using Juwon.Models;

namespace Juwon.Controllers.Standard.Information
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class BuyerController : BaseController
    {
        private readonly IBuyerService buyerService;

        public BuyerController(IBuyerService IBuyerService)
        {
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
            //return View("~/Views/Standard/Information/Buyer/Buyer.cshtml");
            return await Task.Run(() => View("~/Views/Standard/Information/Buyer/Buyer.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await buyerService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await buyerService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BUYER_CREATE)]
        public async Task<ActionResult> CreateBuyer(Buyer obj = null)
        {
            var result = await buyerService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BUYER_MODIFY)]
        public async Task<ActionResult> ModifyBuyer(Buyer obj = null)
        {
            var result = await buyerService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BUYER_MODIFY)]
        public async Task<ActionResult> DeleteBuyer(int BuyerId = 0)
        {
            var result = await buyerService.Delete(BuyerId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.BUYER_MODIFY)]
        public async Task<ActionResult> ActiveBuyer(int BuyerId = 0)
        {
            var result = await buyerService.Active(BuyerId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}