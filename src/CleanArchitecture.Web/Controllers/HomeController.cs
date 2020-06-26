using Microsoft.AspNetCore.Mvc;

namespace RegExLib.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
