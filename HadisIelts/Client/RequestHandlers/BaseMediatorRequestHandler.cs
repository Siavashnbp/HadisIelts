using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers
{
    public class BaseMediatorRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse> where TResponse : ServerResponse
    {
        [Inject]
        private IHttpResponseHandler _httpResponseHandler { get; set; } = default!;
        [Inject]
        private HttpClient _httpClient { get; set; } = default!;
        private readonly string _endpointUri;
        public BaseMediatorRequestHandler(string endpointUri)
        {
            _endpointUri = endpointUri;
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
                case HttpStatusCode.BadRequest:
                    return _httpResponseHandler.HandleBadRequestResponse();
                default:
                    return new ServerResponse { StatusCode = response.StatusCode, Message = response.ReasonPhrase };
            }
        }
    }
}
