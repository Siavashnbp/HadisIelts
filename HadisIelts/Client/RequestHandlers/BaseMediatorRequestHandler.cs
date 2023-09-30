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
        private readonly IHttpResponseHandler _httpResponseHandler;
        private readonly HttpClient _httpClient;
        private readonly string _endpointUri;
        public BaseMediatorRequestHandler(string endpointUri, HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
        {
            _endpointUri = endpointUri;
            _httpClient = httpClient;
            _httpResponseHandler = httpResponseHandler;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (_endpointUri, request, cancellationToken);
            var serverResponse = HandleServerResponse(response);
            var result = default(TResponse);
            if (serverResponse.StatusCode == HttpStatusCode.OK)
            {
                result = await response.Content.ReadFromJsonAsync<TResponse>();
            }
            result!.Message = serverResponse.Message;
            result.StatusCode = serverResponse.StatusCode;
            return result;
        }
        public virtual ServerResponse HandleServerResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return _httpResponseHandler.HandleOkResponse();
                case HttpStatusCode.Unauthorized:
                    return _httpResponseHandler.HandleUnAuthorizedResponse();
                case HttpStatusCode.Forbidden:
                    return _httpResponseHandler.HandleForbidResponse();
                case HttpStatusCode.NotFound:
                    return _httpResponseHandler.HandleContentNotFound();
                case HttpStatusCode.BadRequest:
                    return _httpResponseHandler.HandleBadRequestResponse();
                default:
                    return new ServerResponse { StatusCode = response.StatusCode, Message = response.ReasonPhrase };
            }
        }
    }
}
