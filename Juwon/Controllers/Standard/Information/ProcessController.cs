
using Juwon.Controllers.Base;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Library.Attributes;
using Library.Helpers.Authentication;
using Juwon.Services.Interfaces;
using Juwon.Repository;
using Juwon.Models;
using Library.Helper;

namespace Juwon.Controllers.Standard.Information
{
    [Role(RoleConstants.ROOT, RoleConstants.ADMIN)]
    //[PreventContinuousRequest]
    public class ProcessController : BaseController
    {
        private readonly IProcessService processService;

        public ProcessController(IProcessService IProcessService)
        {
            processService = IProcessService;
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
            return await Task.Run(() => View("~/Views/Standard/Information/Process/Process.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await processService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await processService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PROCESS_CREATE)]
        public async Task<ActionResult> CreateProcess(Process obj = null)
        {
            var result = await processService.CreateTest(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PROCESS_MODIFY)]
        public async Task<ActionResult> ModifyProcess(Process obj = null)
        {
            var result = await processService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PROCESS_DELETE)]
        public async Task<ActionResult> DeleteProcess(int processId = 0)
        {
            var result = await processService.Delete(processId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PROCESS_DELETE)]
        public async Task<ActionResult> ActiveProcess(int processId = 0)
        {
            var result = await processService.Active(processId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}