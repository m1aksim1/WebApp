using Microsoft.AspNetCore.Mvc;
using WebApp;
using System.Globalization;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{

    public class TheoryController : Controller
    {
        private APIClient _client;
        public TheoryController(APIClient client)
        {
            _client = client;
        }
        [HttpGet]
        public IActionResult Theory()
        {
            return View(_client.GetRequest<List<TheoryViewModel>>($"theories/?&get_content={true}"));
        }
        [HttpGet]
        public IActionResult ViewTheory(Guid id)
        {
            return View(_client.GetRequest<TheoryViewModel>($"theory/?aId={id}&get_content={true}"));
        }
        [Authorize]
        public IActionResult CreateTheoryView() 
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult UpdateTheory(Guid id)
        {
            return View(_client.GetRequest<TheoryViewModel>($"theory/?aId={id}&get_content={true}"));
        }
        [Authorize]
        [HttpPost]
        public void UpdateTheory(TheoryViewModel theory)
        {
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

            _client.PutRequest<TheoryViewModel>($"theory/?aId={theory.id}", theory);
        }
        [Authorize]
        [HttpPost]
        public void CreateTheoryView(TheoryViewModel theory)
        {
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

            _client.PostRequest<TheoryViewModel>($"theory/", theory);
            Response.Redirect("Theory");
        }
    }
}
