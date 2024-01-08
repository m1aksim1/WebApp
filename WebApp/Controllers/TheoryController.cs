using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class TheoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
