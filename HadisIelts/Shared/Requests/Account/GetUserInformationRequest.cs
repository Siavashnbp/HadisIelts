using MediatR;

namespace HadisIelts.Shared.Requests.Account
{
    public record GetUserInformationRequest(string userID) :
        IRequest<GetUserInformationRequest.Response>
    {
        public const string EndPointUri = "/api/getUserInfo";
        public record Response(UserInformation userInformation);
    }
    public class UserInformation
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }

    }
}
