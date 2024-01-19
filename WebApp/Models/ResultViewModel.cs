namespace WebApp.Models
{
    public class ResultViewModel
    {
        Guid id { get; set; }
        public string completed_date { get; set; }
        public string name_test { get; set; } = string.Empty;
        public float mark { get; set; }
        public float all_mark { get; set; }
        public string complition_time {  get; set; }
        
        public override string ToString()
        {
            DateTime date = DateTime.Parse(completed_date);

            string formattedDate = date.ToString("yyyy-MM-dd");
            string formattedTime = date.ToString("HH:mm:ss");

            return $"Тест {name_test}\n" +
       $"был пройден {formattedDate} числа.\n" +
       $"в {formattedTime}.\n" +
       $"за {complition_time}\n" +
       $"набранно баллов: {mark}.\n" +
       $"из: {all_mark}.\n" +
       $"что составляет: {mark / all_mark * 100}%.";
        }
    }
}
