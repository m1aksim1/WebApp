namespace WebApp.Models
{
    public class AnswerViewModel
    {
        public string text_answer {  get; set; } = string.Empty;
        public List<string> answers { get; set; } = new List<string>();
    }
}
