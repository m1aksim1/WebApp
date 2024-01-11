using Microsoft.AspNetCore.Mvc;
using SoftwareInstallationClientApp;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult CreateTest()
        {
            string userAgent = HttpContext.Request.Headers.UserAgent;
            return View(APIClient.GetRequest<List<TheoryViewModel>>($"http://localhost:9002/theories/?user_agent={userAgent}" + $"&get_content={true}"));
        }
    }
}