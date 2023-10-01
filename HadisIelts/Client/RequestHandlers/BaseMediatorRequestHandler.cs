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
        public BaseMediatorRequestHandler(HttpClient httpClient, string endpointUri)
        {
            _httpClient = httpClient;
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
            return await HandleError(response);
        }
        public virtual async Task<TResponse> HandleError(HttpResponseMessage response)
        {
            return default(TResponse)!;
        }
    }
}
