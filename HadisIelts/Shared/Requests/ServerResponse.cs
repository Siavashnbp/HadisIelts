using System.Net;

namespace HadisIelts.Shared.Requests
{
    public record ServerResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public string? RedirectUrl { get; set; }
        public ServerResponse()
        {

        }
        public ServerResponse(HttpStatusCode httpStatusCode, string message)
        {
            StatusCode = httpStatusCode;
            Message = message;
        }
        public void AppendToMessage(string newMessage)
        {
            Message += "\n" + newMessage;
        }
    }

}
