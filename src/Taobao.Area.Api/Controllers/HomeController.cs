using Microsoft.AspNetCore.Mvc;

namespace Taobao.Area.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
