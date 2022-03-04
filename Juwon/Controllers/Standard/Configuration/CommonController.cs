using System;
using System.Text;
using System.Web.Mvc;
using Juwon.Controllers.Base;
using System.Threading.Tasks;
using Library.Attributes;
using Library.Helpers.Authentication;
using Juwon.Services.Interfaces;
//using Juwon.DataAccessObjects;
//using Juwon.DataTransferObjects;
using Juwon.Models;
using Library;
using Library.Common;

namespace Juwon.Controllers.Standard.Configuration
{
    [Role(RoleConstants.ROOT)]
    public class CommonController : BaseController
    {
        private readonly ICommonMasterService _commonMasterService;
        private readonly ICommonDetailService _commonDetailService;

        public CommonController(ICommonMasterService CommonMasterService, ICommonDetailService ICommonDetailService)
        {
            _commonMasterService = CommonMasterService;
            _commonDetailService = ICommonDetailService;
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
            //return View("~/Views/Standard/Configuration/Common/Index.cshtml");
            return await Task.Run(() => View("~/Views/Standard/Configuration/Common/Index.cshtml"));
        }

        #region COMMON MASTER

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetCommonMasterList()
        {
            var result = await _commonMasterService.GetAll();
            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_CommonMaster();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.COMMONMASTER_CREATE)]
        public async Task<ActionResult> CreateCommonMaster(CommonMaster model)
        {
            var value = await _commonMasterService.Create(model);
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.COMMONMASTER_MODIFY)]
        public async Task<ActionResult> ModifyCommonMaster(CommonMaster model)
        {
            var value = await _commonMasterService.Modify(model);
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[Role(RoleConstants.ROOT)]
        //[Permission(PermissionConstants.COMMONMASTER_DELETE)]
        //public async Task<ActionResult> DeleteCommonMaster(int id)
        //{
        //    var value = _commonMasterService.DeleteByID(id);
        //    switch (value)
        //    {
        //        case -1:
        //            return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
        //        case 0:
        //            return Json(new { flag = false, message = Resource.ERROR_NotFound }, JsonRequestBehavior.AllowGet);
        //        default:
        //            return Json(new { flag = true, message = Resource.SUCCESS_Delete }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchCommonMaster(string s)
        {
            var result = await _commonMasterService.Search(s);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region COMMON DETAIL

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetCommonDetailList()
        {
            var result = await _commonDetailService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetCommonDetailByMasterCode(string masterCode)
        {
            var result = await _commonDetailService.GetAllByMasterCode(masterCode);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchCommonDetail(string s, string MasterCode = null)
        {
            var result = await _commonDetailService.Search(s, MasterCode);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.COMMONDETAIL_CREATE)]
        public async Task<ActionResult> CreateCommonDetail(/*CommonDetailDTO model*/ CommonDetailModel model)
        {
            var result = await _commonDetailService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.COMMONDETAIL_MODIFY)]
        public async Task<ActionResult> ModifyCommonDetail(CommonDetailModel model)
        {
            var result = await _commonDetailService.Modify(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}