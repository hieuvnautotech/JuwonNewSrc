using System.Threading.Tasks;
using System.Web.Mvc;

namespace Juwon.Controllers
{
    public class AccessDeniedController : Controller
    {
        // GET: AccessDenied
        public async Task<ActionResult> Index()
        {
            //return View("~/Views/401/Index.cshtml");
            return await Task.Run(() => View("~/Views/401/Index.cshtml"));
        }
    }
}