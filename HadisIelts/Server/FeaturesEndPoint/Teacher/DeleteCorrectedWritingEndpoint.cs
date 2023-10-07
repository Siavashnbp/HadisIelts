using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class DeleteCorrectedWritingEndpoint : EndpointBaseAsync
        .WithRequest<DeleteCorrectedWritingRequest>
        .WithActionResult<DeleteCorrectedWritingRequest.Response>
    {
        private readonly ICustomRepositoryServices<CorrectedWritingFile, int> _correctedWritingRepository;
        public DeleteCorrectedWritingEndpoint(ICustomRepositoryServices<CorrectedWritingFile, int> correctedWritingRepository)
        {
            _correctedWritingRepository = correctedWritingRepository;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(DeleteCorrectedWritingRequest.EndpointUri)]
        public override async Task<ActionResult<DeleteCorrectedWritingRequest.Response>> HandleAsync(DeleteCorrectedWritingRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var file = await _correctedWritingRepository.FindByIdAsync(request.Id);
                if (file != null)
                {
                    var wasSuccessful = _correctedWritingRepository.Delete(file);
                    return Ok(new DeleteCorrectedWritingRequest.Response(wasSuccessful));
                }
                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
