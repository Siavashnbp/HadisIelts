using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class UploadCorrectedWritingEndpoint : EndpointBaseAsync
        .WithRequest<UploadCorrectedWritingRequest>
        .WithActionResult<UploadCorrectedWritingRequest.Response>
    {
        private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _dbContext;
        public UploadCorrectedWritingEndpoint(IUserServices userServices,
            ApplicationDbContext dbContext)
        {
            _userServices = userServices;
            _dbContext = dbContext;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(UploadCorrectedWritingRequest.EndpointUri)]
        public override async Task<ActionResult<UploadCorrectedWritingRequest.Response>> HandleAsync(UploadCorrectedWritingRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var writingFile = await _dbContext.WritingCorrectionFiles.FindAsync(request.WritingFileId);
                if (writingFile != null)
                {
                    var userId = _userServices.GetUserIdFromClaims(User.Claims.ToList());
                    var correctedFile = new CorrectedWritingFile
                    {
                        CorrectorId = userId,
                        Data = request.Data,
                        Name = request.Name,
                        UploadDateTime = DateTime.UtcNow,
                        WritingCorrectionFileId = writingFile.Id,
                        WritingCorrectionSubmissionGroupId = writingFile.WritingCorrectionSubmissionGroupId
                    };
                    var submittedCorrectedFile = _dbContext.CorrectedWritingFiles.Add(correctedFile);
                    if (submittedCorrectedFile is not null)
                    {
                        writingFile.CorrectedWritingFileId = submittedCorrectedFile.Entity.Id;
                        _dbContext.WritingCorrectionFiles.Update(writingFile);
                        var changes = _dbContext.SaveChanges();
                        if (changes > 0)
                        {
                            return Ok(new UploadCorrectedWritingRequest.Response(new CorrectedWritingSharedModel
                            {
                                Id = submittedCorrectedFile.Entity.Id,
                                Data = submittedCorrectedFile.Entity.Data,
                                Name = submittedCorrectedFile.Entity.Name,
                                UploadDateTime = submittedCorrectedFile.Entity.UploadDateTime,
                                WritingFileId = submittedCorrectedFile.Entity.WritingCorrectionFileId
                            }));
                        }
                    }
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
