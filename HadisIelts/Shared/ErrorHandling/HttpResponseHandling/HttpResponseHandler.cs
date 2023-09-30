using HadisIelts.Shared.Requests;
using System.Net;

namespace HadisIelts.Shared.ErrorHandling.HttpResponseHandling
{
    public class HttpResponseHandler : IHttpResponseHandler
    {
        public ServerResponse HandleBadRequestResponse() =>
            new ServerResponse(httpStatusCode: HttpStatusCode.BadRequest, message: "There was a problem handling the request");

        public ServerResponse HandleContentNotFound() =>
            new ServerResponse(httpStatusCode: HttpStatusCode.NoContent, message: "Requested data cannot be found");

        public ServerResponse HandleForbidResponse() =>
         new ServerResponse(httpStatusCode: HttpStatusCode.Forbidden, message: "Your request cannot be fulfilled");

        public ServerResponse HandleUnAuthorizedResponse() =>
            new ServerResponse(httpStatusCode: HttpStatusCode.Unauthorized, message: "You don't have permission to access this url");

    }
}
