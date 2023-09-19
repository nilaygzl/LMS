using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
