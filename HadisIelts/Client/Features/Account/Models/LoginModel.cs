namespace HadisIelts.Client.Features.Account.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool KeepSignedIn { get; set; }
        public bool LoginResult { get; set; }
        public string LoginMessage { get; set; }
    }

}
