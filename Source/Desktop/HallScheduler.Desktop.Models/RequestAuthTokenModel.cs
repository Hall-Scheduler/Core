namespace HallScheduler.Desktop.Models
{
    public class RequestAuthTokenModel
    {
        public string Grant_Type { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}
