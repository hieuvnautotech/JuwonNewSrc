using Juwon.Controllers.Base;
using Juwon.Models;
using Juwon.Services.Interfaces;
using Library.Attributes;
using Library.Helper;
using Library.Helpers.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Juwon.Controllers.Standard.Information
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class PartController : BaseController
    {
        private readonly IPartService partService;

        public PartController(IPartService IPartService)
        {
            partService = IPartService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View("~/Views/Standard/Information/Part/Part.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await partService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await partService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PART_CREATE)]
        public async Task<ActionResult> CreatePart(Part obj = null)
        {
            var result = await partService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PART_MODIFY)]
        public async Task<ActionResult> ModifyPart(Part obj = null)
        {
            var result = await partService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PART_DELETE)]
        public async Task<ActionResult> DeletePart(int partId = 0)
        {
            var result = await partService.Delete(partId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PART_DELETE)]
        public async Task<ActionResult> ActivePart(int partId = 0)
        {
            var result = await partService.Active(partId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}