using Juwon.Controllers.Base;
using Juwon.Models;
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

namespace Juwon.Controllers.Standard.Information
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    public class QCMasterController : BaseController
    {
        private readonly IQCMasterService qcMasterService;
        private readonly IQCDetailService qcDetailService;

        public QCMasterController(IQCMasterService IQCMasterService, IQCDetailService IQCDetailService)
        {
            qcMasterService = IQCMasterService;
            qcDetailService = IQCDetailService;
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
            return await Task.Run(() => View("~/Views/Standard/Information/QCMaster/QCMaster.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchActiveQCMaster(string keyWord = "")
        {
            var result = await qcMasterService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAllQCMaster(string keyWord = "")
        {
            var result = await qcMasterService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.QCMASTER_CREATE)]
        public async Task<ActionResult> CreateQCMaster(QCMasterModel obj = null)
        {
            var result = await qcMasterService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.QCMASTER_MODIFY)]
        public async Task<ActionResult> ModifyQCMaster(QCMasterModel obj = null)
        {
            var result = await qcMasterService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.QCMASTER_DELETE)]
        public async Task<ActionResult> DeleteQCMaster(int qcMasterId = 0)
        {
            var result = await qcMasterService.Delete(qcMasterId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.QCMASTER_DELETE)]
        public async Task<ActionResult> ActiveQCMaster(int qcMasterId = 0)
        {
            var result = await qcMasterService.Active(qcMasterId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetQCDetails()
        {
            var result = await qcDetailService.GetActive();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[PreventContinuousRequest]
        public async Task<ActionResult> GetQCDetailsByQCMasterId(int qcMasterId)
        {
            var result = await qcDetailService.GetActiveByQCMasterId(qcMasterId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}