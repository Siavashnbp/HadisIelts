using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
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
        private readonly ICustomRepositoryServices<CorrectedWritingFile, int> _correctedWritingRepository;
        private readonly ICustomRepositoryServices<WritingCorrectionFile, int> _writingCorrectionRepository;
        private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _dbContext;
        public UploadCorrectedWritingEndpoint(ICustomRepositoryServices<CorrectedWritingFile, int> correctedWritingRepository,
            IUserServices userServices,
            ICustomRepositoryServices<WritingCorrectionFile, int> writingCorrectionRepository,
            ApplicationDbContext dbContext)
        {
            _correctedWritingRepository = correctedWritingRepository;
            _userServices = userServices;
            _writingCorrectionRepository = writingCorrectionRepository;
            _dbContext = dbContext;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(UploadCorrectedWritingRequest.EndpointUri)]
        public override async Task<ActionResult<UploadCorrectedWritingRequest.Response>> HandleAsync(UploadCorrectedWritingRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var writingFile = await _writingCorrectionRepository.FindByIdAsync(request.WritingFileId);
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
                        _writingCorrectionRepository.Update(writingFile);
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
                return Problem("Writing file was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
