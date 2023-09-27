namespace HadisIelts.Shared.Models
{
    public class UserInformationSharedModel
    {
        public string Username { get; init; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; init; }
        public string? Skype { get; set; }
        public DateOnly? Birthday { get; set; }
        public UserInformationSharedModel(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
