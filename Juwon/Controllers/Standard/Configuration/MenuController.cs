using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Library.Attributes;
using Library.Helpers.Authentication;
//using Juwon.DataTransferObjects;
//using Juwon.DataAccessObjects;
using Library.Helper;
using Juwon.Controllers.Base;
using Library;
using Library.Common;
using Juwon.Services.Interfaces;
using System.Threading.Tasks;

namespace Juwon.Controllers.Standard.Configuration
{
    [Role(RoleConstants.ROOT)]
    public class MenuController : BaseController
    {
        private readonly ICommonDetailService commonDetailService;
        private readonly IMenuService menuService;

        public MenuController(ICommonDetailService ICommonDetailService, IMenuService IMenuService)
        {
            commonDetailService = ICommonDetailService;
            menuService = IMenuService;
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

        // GET: Menu
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //return View("~/Views/Standard/Configuration/Menu/Index.cshtml");
            return await Task.Run(() => View("~/Views/Standard/Configuration/Menu/Index.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetAll()
        {
            var userRoles = SessionHelper.GetUserSession().Roles;
            ResponseModel<IList<MenuModel>> result;
            if (userRoles.Contains(RoleConstants.ROOT))
            {
                //result = new MenuDAO().GetAll();
                result = await menuService.GetAll();
            }
            else
            {
                //result = new MenuDAO().GetAllExceptRoot();
                result = await menuService.GetAllExceptRoot();
            }

            foreach (var item in result.Data)
            {
                var list = item.FullName.Split('>').ToList();
                var i = list.Count;
                switch (i)
                {
                    case 1:
                        item.PrimaryMenu = list[0];
                        break;
                    case 2:
                        item.PrimaryMenu = list[0];
                        item.SecondaryMenu = list[1];
                        break;
                    default:
                        item.PrimaryMenu = list[0];
                        item.SecondaryMenu = list[1];
                        item.TertiaryMenu = list[2];
                        break;
                }
            }
            return Json(result.Data, JsonRequestBehavior.AllowGet);

            //foreach (var item in result)
            //{
            //    var list = item.FullName.Split('>').ToList();
            //    var i = list.Count;
            //    switch (i)
            //    {
            //        case 1:
            //            item.PrimaryMenu = list[0];
            //            break;
            //        case 2:
            //            item.PrimaryMenu = list[0];
            //            item.SecondaryMenu = list[1];
            //            break;
            //        default:
            //            item.PrimaryMenu = list[0];
            //            item.SecondaryMenu = list[1];
            //            item.TertiaryMenu = list[2];
            //            break;
            //    }
            //}

            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_Menu();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetMenuCategory(string masterCode = "0002")
        {
            //var result = new CommonDetailDAO().GetAllByMasterCode(masterCode);
            var result = await commonDetailService.GetAllByMasterCode(masterCode);
            result.Data = result.Data.Where(x => x.Active == true).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MENU_CREATE)]
        public async Task<ActionResult> Create(MenuModel model)
        {
            //var value = new MenuDAO().Create(model);
            var value = await menuService.Create(model);
            switch (value)
            {
                case -7:
                    return Json(new { flag = false, message = Resource.ERROR_ThirdMenuDuplicated }, JsonRequestBehavior.AllowGet);
                case -6:
                    return Json(new { flag = false, message = Resource.ERROR_SecondMenuNotExist }, JsonRequestBehavior.AllowGet);
                case -5:
                    return Json(new { flag = false, message = Resource.ERROR_SecondMenuDuplicated }, JsonRequestBehavior.AllowGet);
                case -4:
                    return Json(new { flag = false, message = Resource.ERROR_PrimaryMenuNotExist }, JsonRequestBehavior.AllowGet);
                case -3:
                    return Json(new { flag = false, message = Resource.ERROR_PrimaryMenuDuplicated }, JsonRequestBehavior.AllowGet);

                case -2:
                    return Json(new { flag = false, message = Resource.ERROR_CategoryNotBeChosen }, JsonRequestBehavior.AllowGet);
                case -1:
                    return Json(new { flag = false, message = Resource.ERROR_CodeInvalid }, JsonRequestBehavior.AllowGet);
                case 0:
                    return Json(new { flag = false, message = Resource.ERROR_CannotCreate }, JsonRequestBehavior.AllowGet);
                default:
                    var result = await menuService.GetByPrimarySecondary(model.PrimaryMenu, model.SecondaryMenu, model.TertiaryMenu);

                    var list = result.Data.FullName.Split('>').ToList();
                    var i = list.Count;
                    switch (i)
                    {
                        case 1:
                            result.Data.PrimaryMenu = list[0];
                            break;
                        case 2:
                            result.Data.PrimaryMenu = list[0];
                            result.Data.SecondaryMenu = list[1];
                            break;
                        default:
                            result.Data.PrimaryMenu = list[0];
                            result.Data.SecondaryMenu = list[1];
                            result.Data.TertiaryMenu = list[2];
                            break;
                    }

                    return Json(new { flag = true, message = Resource.SUCCESS_Create, result = result.Data }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPut]
        [PreventContinuousRequest]
        [Permission(PermissionConstants.MENU_MODIFY)]
        public async Task<ActionResult> Modify(MenuModel model)
        {
            if (string.IsNullOrEmpty(model.PrimaryMenu) || string.IsNullOrEmpty(model.MenuCategory))
            {
                return Json(new { flag = false, message = Resource.ERROR_CategoryNotBeChosen }, JsonRequestBehavior.AllowGet);
            }

            switch (model.MenuLevel)
            {
                case 1:
                    model.SecondaryMenu = "";
                    model.TertiaryMenu = "";
                    model.MenuLevel2Orderly = 0;
                    model.MenuLevel3Orderly = 0;
                    model.Link = "";
                    break;
                case 2:
                    model.TertiaryMenu = "";
                    model.MenuLevel3Orderly = 0;
                    model.Link = "";
                    break;
                default:
                    break;
            }

            var value = await menuService.Modify(model);
            switch (value)
            {
                case -9:
                    return Json(new { flag = false, message = Resource.ERROR_NotFound }, JsonRequestBehavior.AllowGet);
                case -8:
                    return Json(new { flag = false, message = Resource.ERROR_PrimaryMenuDuplicated }, JsonRequestBehavior.AllowGet);
                case -1:
                    return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                default:
                    return Json(new { flag = true, message = Resource.SUCCESS_Modify }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string s)
        {
            var result = await menuService.Search(s);
            foreach (var item in result.Data)
            {
                var list = item.FullName.Split('>').ToList();
                var i = list.Count;
                switch (i)
                {
                    case 1:
                        item.PrimaryMenu = list[0];
                        break;
                    case 2:
                        item.PrimaryMenu = list[0];
                        item.SecondaryMenu = list[1];
                        break;
                    default:
                        item.PrimaryMenu = list[0];
                        item.SecondaryMenu = list[1];
                        item.TertiaryMenu = list[2];
                        break;
                }
            }

            return Json(result.Data, JsonRequestBehavior.AllowGet);

            //var result = new MenuDAO().Search(s);
            //foreach (var item in result)
            //{
            //    var list = item.FullName.Split('>').ToList();
            //    var i = list.Count;
            //    switch (i)
            //    {
            //        case 1:
            //            item.PrimaryMenu = list[0];
            //            break;
            //        case 2:
            //            item.PrimaryMenu = list[0];
            //            item.SecondaryMenu = list[1];
            //            break;
            //        default:
            //            item.PrimaryMenu = list[0];
            //            item.SecondaryMenu = list[1];
            //            item.TertiaryMenu = list[2];
            //            break;
            //    }
            //}
            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_Menu();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);
        }
    }
}