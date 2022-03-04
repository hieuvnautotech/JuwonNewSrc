using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Library.Helpers.Authentication;
//using Juwon.DataAccessObjects;
using Library.Attributes;
//using Juwon.DataTransferObjects;
using Juwon.Models;
using Library.Helper;
using Juwon.Controllers.Base;
using Library;
using Library.Common;
using System.Threading.Tasks;
using Juwon.Services.Interfaces;

namespace Juwon.Controllers.Standard.Configuration
{
    [Role(RoleConstants.ROOT)]
    public class PermissionController : BaseController
    {
        private readonly IPermissionService permissionService;
        private readonly ICommonDetailService commonDetailService;

        public PermissionController(IPermissionService IPermissionService, ICommonDetailService ICommonDetailService)
        {
            permissionService = IPermissionService;
            commonDetailService = ICommonDetailService;
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

        // GET: Permission
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View("~/Views/Standard/Configuration/Permission/Index.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetAll()
        {
            var userRoles = SessionHelper.GetUserSession().Roles;
            ResponseModel<IList<PermissionModel>> result;
            if (userRoles.Contains(RoleConstants.ROOT))
            {
                result = await permissionService.GetAll();
            }
            else
            {
                result = await permissionService.GetAllExceptRoot();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string s)
        {
            var result = await permissionService.Search(s);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PERMISSION_CREATE)]
        public async Task<ActionResult> Create(Permission model)
        {
            var result = await permissionService.Create(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PERMISSION_MODIFY)]
        public async Task<ActionResult> Modify(Permission model)
        {
            var result = await permissionService.Modify(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.PERMISSION_DELETE)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await permissionService.DeleteByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetPermissions()
        {
            var result = await commonDetailService.GetAllByMasterCode("0003");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}