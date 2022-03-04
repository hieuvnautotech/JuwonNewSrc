using Juwon.Controllers.Base;
using Juwon.Models;
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
    public class SeasonController : BaseController
    {
        private readonly ISeasonService seasonService;
        public SeasonController(ISeasonService ISeasonService)
        {
            seasonService = ISeasonService;
        }
        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View("~/Views/Standard/General/Season/Season.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            try
            {
                var result = await seasonService.SearchActive(keyWord);
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
            var result = await seasonService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.SEASON_CREATE)]
        public async Task<ActionResult> CreateSeason(Season model = null)
        {
            var result = await seasonService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.SEASON_MODIFY)]
        public async Task<ActionResult> ModifySeason(Season model = null)
        {
            var result = await seasonService.Modify(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.SEASON_DELETE)]
        public async Task<ActionResult> DeleteSeason(int seasonId = 0)
        {
            var result = await seasonService.Delete(seasonId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.SEASON_DELETE)]
        public async Task<ActionResult> ActiveSeason(int seasonId = 0)
        {
            var result = await seasonService.Active(seasonId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}