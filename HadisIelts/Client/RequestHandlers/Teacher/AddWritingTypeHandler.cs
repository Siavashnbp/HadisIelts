using HadisIelts.Shared.Requests.Teacher;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class AddWritingTypeHandler : IRequestHandler
        <AddWritingTypeRequest, AddWritingTypeRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public AddWritingTypeHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AddWritingTypeRequest.Response> Handle(AddWritingTypeRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (AddWritingTypeRequest.EndPointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AddWritingTypeRequest.Response>();
                return result;
            }
            return null;
        }
    }
}
