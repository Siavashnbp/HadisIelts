using HadisIelts.Shared.Requests;

namespace HadisIelts.Shared.ErrorHandling.HttpResponseHandling
{
    public interface IExceptionHandler
    {
        public ServerResponse HandleUnAuthorizedResponse();
        public ServerResponse HandleContentNotFound();
    }
}
