using Ardalis.ApiEndpoints;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndpoint.Correction
{
    public class SubmitWritingsEndpoint : EndpointBaseAsync
        .WithRequest<SubmitWritingCorrectionRequest>
        .WithActionResult<int>
    {
        [Authorize]
        [HttpPost(SubmitWritingCorrectionRequest.EndpointUri)]

        public override async Task<ActionResult<int>> HandleAsync(SubmitWritingCorrectionRequest request, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000);
            return Ok(1);
        }
    }
}
