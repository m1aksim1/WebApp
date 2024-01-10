    using Microsoft.AspNetCore.Mvc;
using SoftwareInstallationClientApp;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TheoryController : Controller
    {
        public IActionResult Theory()
        {
            return View();
        }
        public IActionResult CreateTheoryView() 
        {
            return View();
        }
        
        [HttpPost]
        public string CreateTheoryView(TheoryViewModel theory)
        {
            string userAgent = HttpContext.Request.Headers.UserAgent;
            var form = this.ControllerContext.HttpContext.Request.Form;
            var Chpaters = form["Chapters[]"];
            var ChapterContents = form["chapterContents[]"];

            List<ChapterViewModel> combinedArray = new List<ChapterViewModel>();
            for (int i = 0; i < Chpaters.Count; i++)
            {
                combinedArray.Add(new ChapterViewModel
                {
                    name = Chpaters[i],
                    content = ChapterContents[i]
                });
            }
            theory.chapters = combinedArray;

            //todo привести время к формату

            APIClient.PostRequest<TheoryViewModel>($"http://localhost:9002/theory/?user_agent={userAgent}", theory);
            return "das";
        }
    }
}
