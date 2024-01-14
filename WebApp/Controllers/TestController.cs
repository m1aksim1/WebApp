using Microsoft.AspNetCore.Mvc;
using SoftwareInstallationClientApp;
using System.Globalization;
using System.Reflection;
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
        [HttpPost]
        public void CreateTest(TestViewModel model)
        {
            string userAgent = HttpContext.Request.Headers.UserAgent;
            var form = this.ControllerContext.HttpContext.Request.Form;
            var quest_contents = form["quest_content[]"];
            var complition_times = form["complition_time[]"];
            var weights = form["weight[]"];

            if(form["show_answer"] == "on") 
            { 
                model.show_answer = true; 
            }
            else
            {
                model.show_answer = false;
            }
            if (form["shuffle"] == "on")
            {
                model.shuffle = true;
            }
            else
            {
                model.shuffle = false;
            }

            for ( int i = 0; i < quest_contents.Count; i++)
            {
                QuestionViewModel questionViewModel = new QuestionViewModel();
                
                var answers = form["answers"+ (i+1) +"[]"];
                var answersIsTrue = form["answers" + (i + 1) + "IsTrue[]"];

                for ( int j = 0; j < answers.Count; j++)
                {
                    AnswerTest answerTest = new AnswerTest();
                    answerTest.text = answers[j];
                    answerTest.is_correct = bool.Parse(answersIsTrue[j]);
                    
                    questionViewModel.answers_test.Add(answerTest);
                }
                
                var quest = new QuestionViewModel
                {
                    pointer_to_answer = new PointerToAnswer { chapter = new Guid("00bfd2af-b124-43f6-b5e6-e01279d6671e"), start = 0, end = 0 },
                    weight = int.Parse(weights[i]),
                    answers_test = questionViewModel.answers_test,
                    complition_time = complition_times[i]+":00.000Z",
                    name = quest_contents[i],
                };

                model.questions.Add(quest);
            }
            Console.WriteLine(model);
            APIClient.PostRequest<TestViewModel>($"http://localhost:9002/test/?user_agent={userAgent}", model);
        }
        
        [HttpGet]
        public IActionResult Test()
        {
            string userAgent = HttpContext.Request.Headers.UserAgent;
            return View(APIClient.GetRequest<List<TestViewModelView>>($"http://localhost:9002/created_test/?user_agent={userAgent}"));
        }
        [HttpGet]
        public IActionResult PassingTest(Guid id)
        {
            string userAgent = HttpContext.Request.Headers.UserAgent;
            try
            {
                APIClient.PostRequest<TestViewModelView>($"http://localhost:9002/start_test/?user_agent={userAgent}&test_id={id}", new TestViewModelView());
                var Test = APIClient.GetRequest<TestViewModelView>($"http://localhost:9002/test/?user_agent={userAgent}&aId={id}");
                var Quest = APIClient.GetRequest<QuestionViewModel>($"http://localhost:9002/current_question/?user_agent={userAgent}");
                Quest.RemainingTime = (long)TimeSpan.Parse(Test.complition_time).TotalMilliseconds;
                return View(Quest);
            }
            catch (Exception)
            {
                var Test = APIClient.GetRequest<TestViewModelView>($"http://localhost:9002/test/?user_agent={userAgent}&aId={id}");
                var Quest = APIClient.GetRequest<QuestionViewModel>($"http://localhost:9002/current_question/?user_agent={userAgent}");
                Quest.RemainingTime = (long)TimeSpan.Parse(Test.complition_time).TotalMilliseconds;
                return View(Quest);
            }
        }
        [HttpPost]
        public IActionResult PassingTest()
        {
            var answers = new AnswerViewModel();
            answers.text_answer = "почему";
            string userAgent = HttpContext.Request.Headers.UserAgent;
            var form = this.ControllerContext.HttpContext.Request.Form;
            var AnswersIsTrue = form["answersIsTrue[]"];
            var AnswersId = form["answersId[]"];

            for (int i = 0; i < AnswersIsTrue.Count; i++)
            {
                if (bool.Parse(AnswersIsTrue[i])){
                    answers.answers.Add(AnswersId[i]);
                }
            }

            APIClient.PostRequest<AnswerViewModel>($"http://localhost:9002/answer/?user_agent={userAgent}", answers);

            return View(APIClient.GetRequest<QuestionViewModel>($"http://localhost:9002/current_question/?user_agent={userAgent}"));
        }
    }
}