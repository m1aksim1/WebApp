using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ChapterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
