using System.Web.Mvc;
using System.Threading.Tasks;
using Juwon.Services.Interfaces;

namespace Juwon.Controllers.API
{
    public class APIController : Controller
    {
        private readonly IAppService appService;

        public APIController(IAppService IAppService)
        {
            appService = IAppService;
        }

        [HttpGet]
        public async Task<ActionResult> LoadApp()
        {
            var result = await appService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}