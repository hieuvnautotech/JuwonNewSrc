using Juwon.Controllers.Base;
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

namespace Juwon.Controllers.Standard.General
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class MaterialController : BaseController
    {
        private readonly IMaterialService materialService;
        private readonly ICommonDetailService commonDetailService;
        private readonly IPartService partService;
        private readonly IColorService colorService;
        private readonly IMoldService moldService;

        public MaterialController(IMaterialService IMaterialService, ICommonDetailService ICommonDetailService, IPartService IPartService, IColorService IColorService, IMoldService IMoldService)
        {
            materialService = IMaterialService;
            commonDetailService = ICommonDetailService;
            partService = IPartService;
            colorService = IColorService;
            moldService = IMoldService;
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
            return await Task.Run(() => View("~/Views/Standard/General/Material/Material.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await materialService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchActive(string keyWord = "")
        {
            var result = await materialService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MATERIAL_CREATE)]
        public async Task<ActionResult> CreateMaterial(MaterialModel model = null)
        {
            var result = await materialService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MATERIAL_MODIFY)]
        public async Task<ActionResult> ModifyMaterial(MaterialModel model = null)
        {
            var result = await materialService.Modify(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MATERIAL_DELETE)]
        public async Task<ActionResult> DeleteMaterial(int materialId = 0)
        {
            var result = await materialService.Delete(materialId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MATERIAL_DELETE)]
        public async Task<ActionResult> ActiveMaterial(int materialId = 0)
        {
            var result = await materialService.Active(materialId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetMaterialSections()
        {
            var result = await commonDetailService.GetAllByMasterCode("0005");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetMaterialUnits()
        {
            var result = await commonDetailService.GetAllByMasterCode("0006");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetMaterialInOut()
        {
            var result = await commonDetailService.GetAllByMasterCode("0007");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetMaterialType()
        {
            var result = await commonDetailService.GetAllByMasterCode("0008");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetMaterialPart()
        {
            var result = await partService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetMaterialColor()
        {
            var result = await colorService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetMaterialMold()
        {
            var result = await moldService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}