namespace WebApp.Models
{
    public class TheoryViewModel
    {
        public string name { get; set; } = string.Empty;
        public TimeOnly study_time { get; set; }
        public List<ChapterViewModel>? chapters { get; set; }
    }
}