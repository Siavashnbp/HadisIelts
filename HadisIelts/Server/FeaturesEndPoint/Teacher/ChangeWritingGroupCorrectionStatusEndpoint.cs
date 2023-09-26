using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class ChangeWritingGroupCorrectionStatusEndpoint : EndpointBaseAsync
        .WithRequest<ChangeWritingGroupCorrectionStatusRequest>
        .WithActionResult<ChangeWritingGroupCorrectionStatusRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        public ChangeWritingGroupCorrectionStatusEndpoint(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(ChangeWritingGroupCorrectionStatusRequest.EndpointUri)]
        public override async Task<ActionResult<ChangeWritingGroupCorrectionStatusRequest.Response>> HandleAsync(ChangeWritingGroupCorrectionStatusRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var writingGroup = await _dbContext.WritingCorrectionSubmissionGroups.FindAsync(request.Id);
                if (writingGroup is not null)
                {
                    var writings = _dbContext.WritingCorrectionFiles.Where
                        (x => x.WritingCorrectionSubmissionGroupId == writingGroup.Id).ToList();
                    var correctedWritings = _dbContext.CorrectedWritingFiles.Where
                        (x => x.WritingCorrectionSubmissionGroupId == writingGroup.Id).ToList();
                    var areAllCorrected = writings.All(x => correctedWritings.Exists(y => y.WritingCorrectionFileId == x.Id));
                    writingGroup.IsCorrected = areAllCorrected;
                    _dbContext.SaveChanges();
                    return Ok(new ChangeWritingGroupCorrectionStatusRequest.Response(writingGroup.IsCorrected));
                }
                return Problem("Writing group was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
