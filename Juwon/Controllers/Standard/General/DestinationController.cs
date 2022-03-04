
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
    public class DestinationController : BaseController
    {
        private readonly IDestinationService destinationService;

        public DestinationController(IDestinationService IDestinationService)
        {
            destinationService = IDestinationService;
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
            return await Task.Run(() => View("~/Views/Standard/General/Destination/Destination.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await destinationService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await destinationService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetAllActive()
        {
            var result = await destinationService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.DESTINATION_CREATE)]
        public async Task<ActionResult> CreateDestination(Destination obj = null)
        {
            var result = await destinationService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.DESTINATION_MODIFY)]
        public async Task<ActionResult> ModifyDestination(Destination obj = null)
        {
            var result = await destinationService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.DESTINATION_DELETE)]
        public async Task<ActionResult> DeleteDestination(int destinationId = 0)
        {
            var result = await destinationService.Delete(destinationId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.DESTINATION_DELETE)]
        public async Task<ActionResult> ActiveDestination(int destinationId = 0)
        {
            var result = await destinationService.Active(destinationId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}