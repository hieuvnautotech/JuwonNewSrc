using Juwon.Controllers.Base;
using Juwon.Models;
using Juwon.Services.Interfaces;
using Library.Attributes;
using Library.Helper;
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
    public class QCDetailController : BaseController
    {
        private readonly IQCDetailService qcdetailService;

        public QCDetailController(IQCDetailService IQCDetailService)
        {
            qcdetailService = IQCDetailService;
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
            return await Task.Run(() => View("~/Views/Standard/General/QCDetail/QCDetail.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await qcdetailService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.QCDETAIL_CREATE)]
        public async Task<ActionResult> CreateQCDetail(QCDetail obj = null)
        {
            var result = await qcdetailService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.QCDETAIL_MODIFY)]
        public async Task<ActionResult> ModifyQCDetail(QCDetail obj = null)
        {
            var result = await qcdetailService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.QCDETAIL_DELETE)]
        public async Task<ActionResult> DeleteQCDetail(QCDetail obj = null)
        {
            var result = await qcdetailService.Delete(obj.QCDetailId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.QCDETAIL_DELETE)]
        public async Task<ActionResult> ActiveQCDetail(QCDetail obj = null)
        {
            var result = await qcdetailService.Active(obj.QCDetailId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await qcdetailService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}