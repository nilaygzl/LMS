using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
