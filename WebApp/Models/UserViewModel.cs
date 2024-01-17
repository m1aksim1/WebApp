namespace WebApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public string IP {  get; set; } = string.Empty;
    }
}
