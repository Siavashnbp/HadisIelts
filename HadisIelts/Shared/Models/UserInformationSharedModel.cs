namespace HadisIelts.Shared.Models
{
    public class UserInformationSharedModel
    {
        public string Id { get; set; }
        public string Username { get; init; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; init; }
        public string? Skype { get; set; }
        public DateTime? Birthday { get; set; }
        public UserInformationSharedModel(string id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }
    }
}
