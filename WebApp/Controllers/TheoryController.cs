using Microsoft.AspNetCore.Mvc;
using SoftwareInstallationClientApp;
using System.Globalization;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TheoryController : Controller
    {
        [HttpGet]
        public IActionResult Theory()
        {
            string userAgent = HttpContext.Request.Headers.UserAgent;
            return View(APIClient.GetRequest<List<TheoryViewModel>>($"http://localhost:9002/theories/?user_agent={userAgent}"+ $"&get_content={true}"));
        }
        [HttpGet]
        public IActionResult ViewTheory(Guid id)
        {
            return View(APIClient.GetRequest<TheoryViewModel>($"http://localhost:9002/theory/?aId={id}&get_content={true}"));
        }
        public IActionResult CreateTheoryView() 
        {
            //тут заружаем форму элемента
            return View();
        }

        [HttpGet]
        public IActionResult UpdateTheory(Guid id)
        {
            return View(APIClient.GetRequest<TheoryViewModel>($"http://localhost:9002/theory/?aId={id}&get_content={true}"));
        }
        [HttpPost]
        public void UpdateTheory(TheoryViewModel theory)
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
            theory.study_time = theory.StudyTime.ToString("o", CultureInfo.InvariantCulture);

            APIClient.PutRequest<TheoryViewModel>($"http://localhost:9002/theory/?user_agent={userAgent}&aId={theory.id}", theory);
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

            theory.study_time = theory.StudyTime.ToString("o",CultureInfo.InvariantCulture);

            APIClient.PostRequest<TheoryViewModel>($"http://localhost:9002/theory/?user_agent={userAgent}", theory);
            return "das";
        }
    }
}
