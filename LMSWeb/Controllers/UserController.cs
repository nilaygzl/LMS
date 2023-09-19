using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
