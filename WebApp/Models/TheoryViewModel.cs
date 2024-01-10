namespace WebApp.Models
{
    public class TheoryViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public TimeOnly StudyTime { get; set; }
        public string study_time {  get; set; } = string.Empty;
        public List<ChapterViewModel>? chapters { get; set; }
    }
}