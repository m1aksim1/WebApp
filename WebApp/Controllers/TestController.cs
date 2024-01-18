using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebApp;
using System.Globalization;
using System.Reflection;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        private APIClient _client;
        public TestController(APIClient client)
        {
            _client = client;
        }
        [Authorize]
        public IActionResult CreateTest()
        {
            return View(_client.GetRequest<List<TheoryViewModel>>($"theories/?get_content={true}"));
        }
        [HttpPost]
        [Authorize]
        public void CreateTest(TestViewModel model)
        {
            var form = this.ControllerContext.HttpContext.Request.Form;
            var quest_contents = form["quest_content[]"];
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
                    name = quest_contents[i],
                };

                model.questions.Add(quest);
            }
            _client.PostRequest<TestViewModel>($"test/", model);
        }
        
        [HttpGet]
        public IActionResult Test()
        {
            return View(_client.GetRequest<List<TestViewModelView>>($"created_test/"));
        }
        [HttpGet]
        public IActionResult PassingTest(Guid id)
        {
            try
            {
                _client.PostRequest<TestViewModelView>($"start_test/?test_id={id}", new TestViewModelView());
                var Test = _client.GetRequest<TestViewModelView>($"test/?aId={id}");
                var Quest = _client.GetRequest<QuestionViewModel>($"current_question/");
                Quest.RemainingTime = (long)TimeSpan.Parse(Test.complition_time).TotalMilliseconds;
                return View(Quest);
            }
            catch (Exception)
            {
                var Test = _client.GetRequest<TestViewModelView>($"test/?aId={id}");
                var Quest = _client.GetRequest<QuestionViewModel>($"current_question/");
                Quest.RemainingTime = (long)TimeSpan.Parse(Test.complition_time).TotalMilliseconds;
                return View(Quest);
            }
        }
        [HttpPost]
        public IActionResult PassingTest()
        {
            var answers = new AnswerViewModel();
            
            var form = this.ControllerContext.HttpContext.Request.Form;
            var AnswersIsTrue = form["answersIsTrue[]"];
            var AnswersId = form["answersId[]"];
            var TextAnswer = form["TextAnswer"];
            if (TextAnswer.Count > 0)
            {
                answers.text_answer = TextAnswer;
            }
            else
            {
                for (int i = 0; i < AnswersIsTrue.Count; i++)
                {
                    if (bool.Parse(AnswersIsTrue[i]))
                    {
                        answers.answers.Add(AnswersId[i]);
                    }
                }
            }
            try
            {
                _client.PostRequest<AnswerViewModel>($"answer/", answers);
                return View(_client.GetRequest<QuestionViewModel>($"current_question/"));
            }
            catch (Exception ex)
            {
                var Results = _client.GetRequest<List<ResultViewModel>>($"results_test_by_user_easy/");
                
                return View("~/Views/Result/TestResult.cshtml", Results.Last());
            }
        }
    }
}