namespace WebApp.Models
{
    public class TestViewModel
    {
        public string name { get; set; } = string.Empty;
        public Guid theory {  get; set; }
        public List<QuestionViewModel>? questions { get; set; } = new List<QuestionViewModel>();
        public int count_attempts { get; set; }
        public string complition_time { get; set; }
        public bool shuffle { get; set; }
        public bool show_answer { get; set; }
    }
}