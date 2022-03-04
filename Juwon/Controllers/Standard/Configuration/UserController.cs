using System;
using System.Text;
using System.Web.Mvc;
using Library.Attributes;
using Library.Helpers.Authentication;
//using Juwon.DataAccessObjects;
using Juwon.Models;
using Juwon.Controllers.Base;
using Library.Common;
using Library.Helper;
using Library;
using System.Threading.Tasks;
using Juwon.Services.Interfaces;

namespace Juwon.Controllers.Standard.Configuration
{
    [Role(RoleConstants.ROOT, RoleConstants.STANDARD)]
    public class UserController : BaseController
    {
        private readonly IUserInfoService userInfoService;
        private readonly IRoleService roleService;

        public UserController(IUserInfoService IUserInfoService, IRoleService IRoleService)
        {
            userInfoService = IUserInfoService;
            roleService = IRoleService;
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

        // GET: User
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //return View("~/Views/Standard/Configuration/User/Index.cshtml");
            return await Task.Run(() => View("~/Views/Standard/Configuration/User/Index.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetAll()
        {
            //var result = new UserDAO().GetAll();
            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_UserDTO();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);
            var result = await userInfoService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string s)
        {
            //var result = new UserDAO().Search(s);
            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_UserDTO();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);
            var result = await userInfoService.Search(s);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchRole(string s)
        {
            var result = await roleService.Search(s);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetAllRoles()
        {
            var result = await roleService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.USER_CREATE)]
        public async Task<ActionResult> Create(UserInfo model)
        {
            var returnData = new ResponseModel<UserModel>();
            if (string.IsNullOrEmpty(model.Username))
            {
                returnData.ResponseMessage = Resource.ERROR_UsernameCantNotbeNull;
                return Json(returnData, JsonRequestBehavior.AllowGet);
                //return Json(new { flag = false, message = Resource.ERROR_UsernameCantNotbeNull }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                returnData.ResponseMessage = Resource.ERROR_NameCantNotbeNull;
                return Json(returnData, JsonRequestBehavior.AllowGet);
                //return Json(new { flag = false, message = Resource.ERROR_NameCantNotbeNull }, JsonRequestBehavior.AllowGet);
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                model.Password = MD5Encryptor.MD5Hash(model.Password);
            }
            else
            {
                returnData.ResponseMessage = Resource.ERROR_PasswordCantNotbeNull;
                return Json(returnData, JsonRequestBehavior.AllowGet);
                //return Json(new { flag = false, message = Resource.ERROR_PasswordCantNotbeNull }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                model.Email = "";
            }
            if (string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = "";
            }
            if (string.IsNullOrEmpty(model.Address))
            {
                model.Address = "";
            }

            //var value = new UserDAO().Create(model);
            var value = await userInfoService.Create(model);
            switch (value)
            {
                case -1:
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                //return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                case -2:
                    returnData.ResponseMessage = Resource.ERROR_DuplicatedName;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                //return Json(new { flag = false, message = Resource.ERROR_DuplicatedName }, JsonRequestBehavior.AllowGet);
                default:
                    var result = await userInfoService.GetByUserName(model.Username);
                    return Json(result, JsonRequestBehavior.AllowGet);
                    //return Json(new { flag = true, message = Resource.SUCCESS_Modify, result = new UserDAO().GetByUsername(model.Username) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.USER_MODIFY)]
        public async Task<ActionResult> Modify(UserInfo model)
        {
            var returnData = new ResponseModel<UserModel>();
            if (string.IsNullOrEmpty(model.Username))
            {
                returnData.ResponseMessage = Resource.ERROR_UsernameCantNotbeNull;
                return Json(returnData, JsonRequestBehavior.AllowGet);
                //return Json(new { flag =false, message = Resource.ERROR_UsernameCantNotbeNull }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                returnData.ResponseMessage = Resource.ERROR_NameCantNotbeNull;
                return Json(returnData, JsonRequestBehavior.AllowGet);
                //return Json(new { flag = false, message = Resource.ERROR_NameCantNotbeNull }, JsonRequestBehavior.AllowGet);
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                model.Password = MD5Encryptor.MD5Hash(model.Password);
            }

            //var value = new UserDAO().Modify(model);

            var value = await userInfoService.Modify(model);
            switch (value)
            {
                case -1:
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                //return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                default:
                    var result = await userInfoService.GetByID(model.ID);
                    return Json(result, JsonRequestBehavior.AllowGet);
                    //return Json(new { flag = true, message = Resource.SUCCESS_Modify, result = new UserDAO().GetByID(model.ID) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetRoleIDByUserID(int id)
        {
            var result = await roleService.GetRoleIDByUserID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        public async Task<ActionResult> AuthorizeRoles(AuthorizeModel model)
        {
            if (model.ID == null /*|| model.AuthorizeIDs.Count == 0*/)
            {
                return Json(new { flag = false, message = Resource.WARN_NotSelectOnGrid }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var value = new UserDAO().AuthorizeRoles(model);
                var value = await userInfoService.AuthorizeRoles(model);
                switch (value)
                {
                    case -1:
                        return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                    default:
                        return Json(new { flag = true, message = Resource.SUCCESS_Authorization }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}