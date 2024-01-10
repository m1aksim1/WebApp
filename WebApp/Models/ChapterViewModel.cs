namespace WebApp.Models
{
    public class ChapterViewModel
    {
        Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
    }
}