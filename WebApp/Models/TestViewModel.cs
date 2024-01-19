namespace WebApp.Models
{
    public class TestViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public Guid theory { get; set; }
        public List<QuestionViewModel>? questions { get; set; } = new List<QuestionViewModel>();
        public int count_attempts { get; set; }
        public string complition_time { get; set; }
        public bool shuffle { get; set; }
        public bool show_answer { get; set; }

    }
    public class TestViewModelView
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public TheoryViewModel theory { get; set; }
        public List<QuestionViewModel>? questions { get; set; } = new List<QuestionViewModel>();
        public int count_attempts { get; set; }
        public string complition_time { get; set; }
        public bool shuffle { get; set; }
        public bool show_answer { get; set; }
       
    }
    public class TestViewModelViewAdapter
    {
        private TestViewModelView _testViewModelView;

        public TestViewModelViewAdapter(TestViewModelView testViewModelView)
        {
            _testViewModelView = testViewModelView;
        }

        public TestViewModel GetViewModel()
        {
            return new TestViewModel
            {
                id = _testViewModelView.id,
                name = _testViewModelView.name,
                theory = _testViewModelView.theory.id,
                questions = _testViewModelView.questions,
                count_attempts = _testViewModelView.count_attempts,
                complition_time = _testViewModelView.complition_time,
                shuffle = _testViewModelView.shuffle,
                show_answer = _testViewModelView.show_answer,
            };
        }
    }
}