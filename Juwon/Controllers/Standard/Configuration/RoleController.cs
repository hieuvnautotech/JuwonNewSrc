using System;
using System.Text;
using System.Web.Mvc;
//using Juwon.DataAccessObjects;
using Library.Attributes;
//using Juwon.DataTransferObjects;
using Library.Helpers.Authentication;
using Library.Common;
using Library.Helper;
using Juwon.Controllers.Base;
using Library;
using Juwon.Services.Interfaces;
using System.Threading.Tasks;

namespace Juwon.Controllers.Standard.Configuration
{
    [Role(RoleConstants.ROOT)]
    public class RoleController : BaseController
    {
        private readonly IRoleService roleService;
        private readonly IMenuService menuService;
        private readonly ICommonDetailService commonDetailService;
        public RoleController(IRoleService IRoleService, IMenuService IMenuService, ICommonDetailService ICommonDetailService)
        {
            roleService = IRoleService;
            menuService = IMenuService;
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

        // GET: Role
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //return View("~/Views/Standard/Configuration/Role/Index.cshtml");
            return await Task.Run(() => View("~/Views/Standard/Configuration/Role/Index.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetAll()
        {
            //var result = new RoleDAO().GetAll();
            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_Role();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);

            var result = await roleService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetMenuByRoleID(int id)
        {
            //var result = new MenuDAO().GetMenuIDByRoleID(id);
            //return Json(new { result }, JsonRequestBehavior.AllowGet);

            var result = await menuService.GetMenuIDByRoleID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetPermissionByRoleID(int id)
        {
            //var result = new MenuDAO().GetPermissionIDByRoleID(id);
            //return Json(new { result }, JsonRequestBehavior.AllowGet);

            var result = await menuService.GetPermissionIDByRoleID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.ROLE_CREATE)]
        public async Task<ActionResult> Create(RoleModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Description))
            {
                return Json(new { flag = false, message = Resource.ERROR_FullFillTheForm }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(model.RoleCategory))
            {
                return Json(new { flag = false, message = Resource.ERROR_CategoryNotBeChosen }, JsonRequestBehavior.AllowGet);
            }

            //var value = new RoleDAO().Create(model);
            var value = await roleService.Create(model);

            switch (value)
            {
                case -1:
                    return Json(new { flag = false, message = Resource.ERROR_DuplicatedName }, JsonRequestBehavior.AllowGet);
                case 0:
                    return Json(new { flag = false, message = Resource.ERROR_CannotCreate }, JsonRequestBehavior.AllowGet);
                default:
                    var result = await roleService.GetByName(model.Name);
                    return Json(new { flag = true, message = Resource.SUCCESS_Create, result }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.ROLE_MODIFY)]
        public async Task<ActionResult> AuthorizeMenus(RoleMenuModel model)
        {
            if (model.RoleID == null || model.MenuIDs.Count == 0)
            {
                return Json(new { flag = false, message = Resource.WARN_NotSelectOnGrid }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var value = new RoleDAO().AuthorizeMenus(model);
                var value = await roleService.AuthorizeMenus(model);
                switch (value)
                {
                    case -1:
                        return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                    default:
                        return Json(new { flag = true, message = Resource.SUCCESS_Authorization }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.ROLE_MODIFY)]
        public async Task<ActionResult> AuthorizePermissions(RoleMenuModel model)
        {
            if (model.RoleID == null || model.PermissionIDs.Count == 0)
            {
                return Json(new { flag = false, message = Resource.WARN_NotSelectOnGrid }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var value = new RoleDAO().AuthorizePermissions(model);
                var value = await roleService.AuthorizePermissions(model);
                switch (value)
                {
                    case -1:
                        return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                    default:
                        return Json(new { flag = true, message = Resource.SUCCESS_Authorization }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string s)
        {
            //var result = new RoleDAO().Search(s);

            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_Role();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);
            var result = await roleService.Search(s);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetRoleCategory()
        {
            var result = await commonDetailService.GetAllByMasterCode("0001");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}