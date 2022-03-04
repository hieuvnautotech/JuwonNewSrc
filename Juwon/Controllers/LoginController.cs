using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Juwon.Hubs;
using Juwon.Services.Interfaces;
//using Juwon.DataTransferObjects;
using Library.Helper;
using Juwon.Repository;
using Library.Common;
using Library;
using Juwon.Controllers.Base;
using System.Web.Security;
using Library.Attributes;

namespace Juwon.Controllers
{
    public class LoginController : LoginBaseController
    {
        private readonly ILoginService loginService;
        private readonly IUserInfoService userInfoService;
        public LoginController(ILoginService ILoginService, IUserInfoService IUserInfoService)
        {
            loginService = ILoginService;
            userInfoService = IUserInfoService;
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
            LoginModel model = new LoginModel();
            return await Task.Run(() => View(model));
        }

        [HttpPost]
        [PreventContinuousRequest]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await loginService.CheckLogin(model);
                int check = result;
                switch (result)
                {
                    case 1:
                        //set session
                        var userDTO = await userInfoService.GetUserByUserName(model.UserName);
                        userDTO.SessionId = Session.SessionID.ToString();
                        HttpContext.Cache[userDTO.UserName] = Session.SessionID.ToString();

                        if (userDTO.IpAddress != "::1" && userDTO.IpAddress != "127.0.0.1")
                        {
                            await userInfoService.RecordUserLog(userDTO.ID, userDTO.IpAddress, userDTO.SessionId); //using for kick out user 
                        }

                        SessionHelper.SetUserSession(userDTO);

                        ////set cookie
                        //SetAuthenticationCookie(model);

                        var defaultController = userDTO.Menus.FirstOrDefault(x => x.MenuLevel == 3).Link.Replace("/", "");


                        return RedirectToAction("Index", defaultController);

                    case 2:
                        ModelState.AddModelError("", Resource.ERROR_WrongPassword);
                        break;

                    case -1:
                        ModelState.AddModelError("", Resource.ERROR_AccountBlocked);
                        break;

                    case -3:
                        ModelState.AddModelError("", Resource.ERROR_AccountUnAuthorized);
                        break;

                    default:
                        ModelState.AddModelError("", Resource.ERROR_AccountNotRegistered);
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", Resource.ERROR_EnterUsernamePassword);
            }
            return View(model);
        }

        //[HttpPost]
        //public async Task<ActionResult> loginApp(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var result = new UserDAO().CheckLogin(model.UserName, MD5Encryptor.MD5Hash(model.Password));
        //        var result = await loginService.CheckLogin(model);
        //        switch (result)
        //        {
        //            case 1:
        //                //set session
        //                var userDTO = new UserDAO().GetUserByUserName(model.UserName);

        //                SessionHelper.SetUserSession(userDTO);

        //                ////set cookie
        //                //SetAuthenticationCookie(model);

        //                var defaultController = userDTO.Menus.FirstOrDefault(x => x.MenuLevel == 3).Name;

        //                return Json(new { flag = true, message = Resource.SUCCESS_Login, defaultController, userDTO }, JsonRequestBehavior.AllowGet);

        //            case 2:
        //                return Json(new { flag = false, message = Resource.ERROR_WrongPassword }, JsonRequestBehavior.AllowGet);

        //            case -1:
        //                return Json(new { flag = false, message = Resource.ERROR_AccountBlocked }, JsonRequestBehavior.AllowGet);

        //            case -3:
        //                return Json(new { flag = false, message = Resource.ERROR_AccountUnAuthorized }, JsonRequestBehavior.AllowGet);

        //            default:
        //                return Json(new { flag = false, message = Resource.ERROR_AccountNotRegistered }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { flag = false, message = Resource.ERROR_EnterUsernamePassword }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public async Task<ActionResult> Logout()
        {
            FormsAuthentication.SignOut();
            UserModel user = SessionHelper.GetUserSession();
            if (user != null)
            {
                //Session.RemoveAll();
                int userId = user.ID;
                string ipAddress = user.IpAddress;
                string sessionId = user.SessionId;
                await loginService.Logout(userId, ipAddress, sessionId);
            }

            Session.Remove(CommonConstants.USER_SESSION);

            return RedirectToAction("Index", "Login");
        }

        //// Set cookie
        //public void SetAuthenticationCookie(LoginDTO model)
        //{
        //    if (!model.RememberMe)
        //    {
        //        FormsAuthentication.SetAuthCookie(model.UserName, false);
        //        return;
        //    }
        //    const int timeout = 2880; // Timeout is in minutes, 525600 = 365 days; 1 day = 1440.
        //    var ticket = new FormsAuthenticationTicket(model.UserName, model.RememberMe, timeout);//ticket.

        //    string encrypted = FormsAuthentication.Encrypt(ticket);
        //    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
        //    {
        //        Expires = System.DateTime.Now.AddMinutes(timeout),
        //        HttpOnly = true
        //    };
        //    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        //}

        public async Task<ActionResult> ChangePassword(LoginModel model)
        {
            var user = SessionHelper.GetUserSession();

            var result = await userInfoService.ChangePassword(user.UserName, MD5Encryptor.MD5Hash(model.Password));
            var returnData = new ResponseModel<int>();
            switch (result)
            {
                case 0:
                    returnData.HttpResponseCode = 500;
                    break;
                default:
                    returnData.IsSuccess = true;
                    returnData.Data = result;
                    returnData.ResponseMessage = Resource.SUCCESS_Success;
                    break;
            }

            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}