namespace WebApp.Models
{
    public class TestViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public object theory { get; set; }
        public List<QuestionViewModel>? questions { get; set; } = new List<QuestionViewModel>();
        public int count_attempts { get; set; }
        public string complition_time { get; set; }
        public bool shuffle { get; set; }
        public bool show_answer { get; set; }

        public static TestViewModel CreateFromTheoryViewModel(TheoryViewModel theory)
        {
            return new TestViewModel
            {
                theory = theory,
            };
        }

        public static TestViewModel CreateFromGuid(Guid theoryId)
        {
            return new TestViewModel
            {
                theory = theoryId,
            };
        }
    }

}