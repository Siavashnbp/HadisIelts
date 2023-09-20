using Ardalis.ApiEndpoints;
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
        public UploadCorrectedWritingEndpoint(ICustomRepositoryServices<CorrectedWritingFile, int> correctedWritingRepository,
            IUserServices userServices,
            ICustomRepositoryServices<WritingCorrectionFile, int> writingCorrectionRepository)
        {
            _correctedWritingRepository = correctedWritingRepository;
            _userServices = userServices;
            _writingCorrectionRepository = writingCorrectionRepository;
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
                    var submittedCorrectedFile = _correctedWritingRepository.Insert(correctedFile);

                    return Ok(new CorrectedWritingSharedModel
                    {
                        ID = submittedCorrectedFile.ID,
                        Data = submittedCorrectedFile.Data,
                        Name = submittedCorrectedFile.Name,
                        UploadDateTime = submittedCorrectedFile.UploadDateTime,
                        WritingFileID = submittedCorrectedFile.WritingCorrectionFileID
                    });
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
