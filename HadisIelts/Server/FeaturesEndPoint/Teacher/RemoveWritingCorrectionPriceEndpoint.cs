using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class RemoveWritingCorrectionPriceEndpoint : EndpointBaseAsync
        .WithRequest<RemoveWritingCorrectionPriceRequest>
        .WithActionResult<RemoveWritingCorrectionPriceRequest.Response>
    {
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int> _writingCorrectionPriceRepository;
        public RemoveWritingCorrectionPriceEndpoint(
            ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionPriceRepository)
        {
            _writingCorrectionPriceRepository = writingCorrectionPriceRepository;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(RemoveWritingCorrectionPriceRequest.EndPointUri)]
        public override async Task<ActionResult<RemoveWritingCorrectionPriceRequest.Response>> HandleAsync(RemoveWritingCorrectionPriceRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await _writingCorrectionPriceRepository.FindByIDAsync(request.ID);
                var result = _writingCorrectionPriceRepository.Delete(item);
                return Ok(new RemoveWritingCorrectionPriceRequest.Response(result));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
