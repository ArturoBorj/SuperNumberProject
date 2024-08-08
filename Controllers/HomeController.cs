using Microsoft.AspNetCore.Mvc;

namespace SuperNumberProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
