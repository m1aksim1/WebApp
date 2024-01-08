using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.HttpOverrides;
using SoftwareInstallationClientApp;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Enter()
        {
            var coock = HttpContext.Response.Cookies;
            return View();
        }
        [HttpPost]
        public string Enter(string pohui)
        {
            string userAgent = HttpContext.Request.Headers.UserAgent;

            APIClient.PostRequest<UserViewModel>($"http://localhost:9002/?user_agent={userAgent}", new UserViewModel { UserAgent = userAgent});

            return "das";
        }
    }
}