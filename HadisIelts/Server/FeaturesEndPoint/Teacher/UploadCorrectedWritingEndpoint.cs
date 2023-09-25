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
                var writingFile = await _writingCorrectionRepository.FindByIDAsync(request.WritingFileID);
                if (writingFile != null)
                {
                    var userID = _userServices.GetUserIDFromClaims(User.Claims.ToList());
                    var correctedFile = new CorrectedWritingFile
                    {
                        CorrectorID = userID,
                        Data = request.Data,
                        Name = request.Name,
                        UploadDateTime = DateTime.UtcNow,
                        WritingCorrectionFileID = writingFile.ID,
                        WritingCorrectionSubmissionGroupID = writingFile.WritingCorrectionSubmissionGroupID
                    };
                    var submittedCorrectedFile = _dbContext.CorrectedWritingFiles.Add(correctedFile);
                    if (submittedCorrectedFile is not null)
                    {
                        writingFile.CorrectedWritingFileID = submittedCorrectedFile.Entity.ID;
                        _writingCorrectionRepository.Update(writingFile);
                        return Ok(new UploadCorrectedWritingRequest.Response(new CorrectedWritingSharedModel
                        {
                            ID = submittedCorrectedFile.Entity.ID,
                            Data = submittedCorrectedFile.Entity.Data,
                            Name = submittedCorrectedFile.Entity.Name,
                            UploadDateTime = submittedCorrectedFile.Entity.UploadDateTime,
                            WritingFileID = submittedCorrectedFile.Entity.WritingCorrectionFileID
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
