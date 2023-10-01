using HadisIelts.Shared.Requests;
using System.Net;

namespace HadisIelts.Shared.ErrorHandling.HttpResponseHandling
{
    public class ExceptionHandler : IExceptionHandler
    {
        public ServerResponse HandleContentNotFound() =>
            new ServerResponse(httpStatusCode: HttpStatusCode.NoContent, message: "Requested data cannot be founs");
        public ServerResponse HandleUnAuthorizedResponse() =>
            new ServerResponse(httpStatusCode: HttpStatusCode.Unauthorized, message: "You don't have permission to access this url");

    }
}
