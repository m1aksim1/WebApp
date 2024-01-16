namespace WebApp.Models
{
    public class ResultViewModel
    {
        Guid Id { get; set; }
        public string completed_date { get; set; }
        public string name_test { get; set; } = string.Empty;
        public int mark { get; set; }
        public int all_mark { get; set; }
        public string complition_time {  get; set; }
    }
}
