using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
