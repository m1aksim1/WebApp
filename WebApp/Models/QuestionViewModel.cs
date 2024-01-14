namespace WebApp.Models
{
    public class QuestionViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public TimeOnly ComplitionTime { get; set; }
        public string? complition_time { get; set; }
        public int? weight { get; set; }
        public PointerToAnswer pointer_to_answer { get; set; } = new PointerToAnswer();
        public List<AnswerTest> answers_test { get; set; } = new List<AnswerTest>();

        public long RemainingTime { get; set; }
    }
    public class PointerToAnswer
    {
        public int start { get; set; }
        public int end { get; set; }
        public Guid chapter { get; set; }
    }
    public class AnswerTest
    {
        public Guid id { get; set; }
        public string text { get; set; } = string.Empty;
        public bool is_correct { get; set; } = false;
    }
}