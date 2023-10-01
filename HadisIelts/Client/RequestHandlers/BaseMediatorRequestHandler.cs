using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests;
using MediatR;
using System.Net;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers
{
    public class BaseMediatorRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse> where TResponse : ServerResponse
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUri;
        private readonly IHttpResponseHandler _httpResponseHandler;
        public BaseMediatorRequestHandler(HttpClient httpClient, string endpointUri, IHttpResponseHandler httpResponseHandler)
        {
            _httpClient = httpClient;
            _endpointUri = endpointUri;
            _httpResponseHandler = httpResponseHandler;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (_endpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResponse>();
                result!.StatusCode = HttpStatusCode.OK;
                result.Message = "Success";
                return result;
            }
            else
            {
                var error = HandleError(response);
                var result = Activator.CreateInstance<TResponse>();
                result.StatusCode = error.StatusCode;
                result.Message = error.Message;
                result.RedirectUrl = string.Empty;
                return result;
            }

        }
        public virtual ServerResponse HandleError(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return _httpResponseHandler.HandleUnAuthorizedResponse();
                case HttpStatusCode.Forbidden:
                    return _httpResponseHandler.HandleForbidResponse();
                case HttpStatusCode.NotFound:
                    return _httpResponseHandler.HandleContentNotFound();
                default:
                    return new ServerResponse { StatusCode = response.StatusCode, Message = response.ReasonPhrase };
            }
        }
    }
}
