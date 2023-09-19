using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
