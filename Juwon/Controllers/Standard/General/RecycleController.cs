using Juwon.Models;
using Juwon.Repository;
using Juwon.Services.Interfaces;
using Library.Attributes;
using Library.Helpers.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Juwon.Controllers.Standard.General
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class RecycleController : Controller
    {
      
        private readonly IRecycleService recycleService;
        public RecycleController(IRecycleService IRecycleService)
        {
            recycleService = IRecycleService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View("~/Views/Standard/General/Recycle/Recycle.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            try
            {
                var result = await recycleService.SearchActive(keyWord);
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
            var result = await recycleService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.RECYCLE_CREATE)]
        public async Task<ActionResult> CreateRecycle(Recycle model = null)
        {
            var result = await recycleService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.RECYCLE_MODIFY)]
        public async Task<ActionResult> ModifyRecycle(Recycle model = null)
        {
            var result = await recycleService.Modify(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.RECYCLE_DELETE)]
        public async Task<ActionResult> DeleteRecycle(int recycleId = 0)
        {
            var result = await recycleService.Delete(recycleId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.RECYCLE_DELETE)]
        public async Task<ActionResult> ActiveRecycle(int recycleId = 0)
        {
            var result = await recycleService.Active(recycleId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}