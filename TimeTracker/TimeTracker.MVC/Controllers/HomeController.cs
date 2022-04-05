using Microsoft.AspNetCore.Mvc;

namespace TimeTracker.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}