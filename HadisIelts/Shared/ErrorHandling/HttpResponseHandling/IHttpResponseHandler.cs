using HadisIelts.Shared.Requests;

namespace HadisIelts.Shared.ErrorHandling.HttpResponseHandling
{
    public interface IHttpResponseHandler
    {
        public ServerResponse HandleUnAuthorizedResponse();
        public ServerResponse HandleContentNotFound();
        public ServerResponse HandleForbidResponse();
        public ServerResponse HandleBadRequestResponse();
    }
}
