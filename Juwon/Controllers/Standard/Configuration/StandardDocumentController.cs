using System;
using System.Text;
using System.Web.Mvc;
using Library.Attributes;
//using Juwon.DataAccessObjects;
using Library.Helper;
using Library.Helpers.Authentication;
using Juwon.Controllers.Base;
using Library;
using System.Threading.Tasks;
using Juwon.Services.Interfaces;

namespace Juwon.Controllers.Standard.Configuration
{
    [Role(RoleConstants.ROOT)]
    public class StandardDocumentController : BaseController
    {
        private readonly IUserManualService userManualService;
        private readonly IMenuService menuService;

        public StandardDocumentController(IUserManualService IUserManualService, IMenuService IMenuService)
        {
            userManualService = IUserManualService;
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

        // GET: StandardDocument
        
        public async Task<ActionResult> Index()
        {
            //return View("~/Views/Standard/Configuration/UserManual/UserManual.cshtml");
            return await Task.Run(() => View("~/Views/Standard/Configuration/UserManual/UserManual.cshtml"));
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> getGuidingList()
        {
            //var result = new UserManualDAO().GetAll();
            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_UserGuide();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);

            var result = await userManualService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetGuidingById(int Id)
        {
            //var result = new UserManualDAO().GetByID(Id);
            //return Json(new { result }, JsonRequestBehavior.AllowGet);

            var result = await userManualService.GetByID(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PreventContinuousRequest]
        [ValidateInput(false)]
        public async Task<ActionResult> Insert()
        {

            string name = Request["Name"] == null ? "" : Request["Name"].Trim();
            string content = Request["Content"] == null ? "" : Request["Content"].Trim();
            string menuCode = Request["MenuCode"] == null ? "" : Request["MenuCode"].Trim();
            string menuLevel = Request["MenuLevel"] == null ? "" : Request["MenuLevel"].Trim();
            string languageCode = Request["LanguageCode"] == null ? "" : Request["LanguageCode"].Trim();
            string description = Request["Description"] == null ? "" : Request["Description"].Trim();


            //var value = new UserManualDAO().Insert(name, content, menuCode, menuLevel, languageCode, description);
            var value = await userManualService.Insert(name, content, menuCode, menuLevel, languageCode, description);

            switch (value)
            {
                case -1:
                    return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                case -2:
                    return Json(new { flag = false, message = Resource.ERROR_FullFillTheForm }, JsonRequestBehavior.AllowGet);
                case 0:
                    return Json(new { flag = false, message = Resource.ERROR_DuplicatedCode }, JsonRequestBehavior.AllowGet);
                default:
                    return Json(new
                    {
                        flag = true,
                        message = Resource.SUCCESS_Create,
                        //result = new UserManualDAO().Search2(languageCode, menuCode)
                        result = await userManualService.Search(languageCode, menuCode)
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPut]
        [PreventContinuousRequest]
        [ValidateInput(false)]
        public async Task<ActionResult> Update()
        {
            string Id = Request["Id"] == null ? "" : Request["Id"].Trim();
            string name = Request["Name"] == null ? "" : Request["Name"].Trim();
            string content = Request["Content"] == null ? "" : Request["Content"].Trim();
            string menuCode = Request["MenuCode"] == null ? "" : Request["MenuCode"].Trim();
            string menuLevel = Request["MenuLevel"] == null ? "" : Request["MenuLevel"].Trim();
            string languageCode = Request["LanguageCode"] == null ? "" : Request["LanguageCode"].Trim();
            string description = Request["Description"] == null ? "" : Request["Description"].Trim();

            //var value = new UserManualDAO().Update(Id, name, content, menuCode, menuLevel, languageCode, description);
            var value = await userManualService.Update(Id, name, content, menuCode, menuLevel, languageCode, description);
            switch (value)
            {
                case -1:
                    return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                case -2:
                    return Json(new { flag = false, message = Resource.ERROR_FullFillTheForm }, JsonRequestBehavior.AllowGet);
                case -3:
                    return Json(new { flag = false, message = Resource.ERROR_NotFound }, JsonRequestBehavior.AllowGet);
                case 0:
                    return Json(new { flag = false, message = Resource.ERROR_DuplicatedCode }, JsonRequestBehavior.AllowGet);
                default:
                    int x = Int32.Parse(Id);
                    return Json(new
                    {
                        flag = true,
                        message = Resource.SUCCESS_Create,
                        //result = new UserManualDAO().GetByID(x)
                        result = await userManualService.GetByID(x)
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search(string languageCode, string menuCode, string menuName)
        {
            //var result = new UserManualDAO().Search(languageCode, menuCode, menuName);
            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_UserGuide();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);
            var result = await userManualService.Search2(languageCode, menuCode, menuName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> Search2(string languageCode, string menuCode)
        {
            //var result = new UserManualDAO().Search2(languageCode, menuCode);
            //var multiLangModel = new MultiLanguagesHelper().MultiLanguagesHelper_UserGuide();
            //return Json(new { result, multiLangModel }, JsonRequestBehavior.AllowGet);

            var result = await userManualService.Search(languageCode, menuCode);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        [PreventContinuousRequest]
        public async Task<ActionResult> Delete(int id)
        {
            //var value = new UserManualDAO().DeleteByID(id);
            var value = await userManualService.DeleteByID(id);
            switch (value)
            {
                case -1:
                    return Json(new { flag = false, message = Resource.ERROR_SystemError }, JsonRequestBehavior.AllowGet);
                case 0:
                    return Json(new { flag = false, message = Resource.ERROR_NotFound }, JsonRequestBehavior.AllowGet);
                default:
                    return Json(new { flag = true, message = Resource.SUCCESS_Delete }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> GetAllMenu()
        {
            var result = await menuService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [PreventContinuousRequest]
        public async Task<ActionResult> SearchMenu(string s)
        {
            var result = await menuService.Search(s);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}