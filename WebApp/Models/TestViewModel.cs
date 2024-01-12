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
        
        TestViewModel GetViewModel()
        {
            return new TestViewModel 
            { 
                id = id,
                name = name, 
                theory = theory.id,
                questions = questions,
                count_attempts = count_attempts,
                complition_time = complition_time,
                shuffle = shuffle,
                show_answer = show_answer,
            };
        }
    }
}