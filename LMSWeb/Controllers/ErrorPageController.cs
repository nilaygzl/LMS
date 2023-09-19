using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
