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
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService IDepartmentService)
        {
            departmentService = IDepartmentService;
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
            return await Task.Run(() => View("~/Views/Standard/General/Department/Department.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string keyWord = "")
        {
            var result = await departmentService.SearchActive(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.DEPARTMENT_CREATE)]
        public async Task<ActionResult> CreateDepartment(Department obj = null)
        {
            var result = await departmentService.Create(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.DEPARTMENT_MODIFY)]
        public async Task<ActionResult> ModifyDepartment(Department obj = null)
        {
            var result = await departmentService.Modify(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.DEPARTMENT_DELETE)]
        public async Task<ActionResult> DeleteDepartment(int departmentid = 0)
        {
            var result = await departmentService.Delete(departmentid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchAll(string keyWord = "")
        {
            var result = await departmentService.SearchAll(keyWord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.AREACATEGORY_DELETE)]
        public async Task<ActionResult> ActiveDepartment(int departmentid = 0)
        {
            var result = await departmentService.Active(departmentid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}