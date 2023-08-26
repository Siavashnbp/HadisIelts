using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndpoint.Correction
{
    public class SubmitWritingsEndpoint : EndpointBaseAsync
        .WithRequest<UploadProcessedWritingFilesRequest>
        .WithActionResult<UploadProcessedWritingFilesRequest.Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomRepositoryServices<SubmittedWritingCorrectionFiles, string>
            _submittedCorrectionFilesRepository;
        private readonly ICustomRepositoryServices<WritingCorrectionFile, int>
            _writingCorrectionFileRepository;
        public SubmitWritingsEndpoint(UserManager<ApplicationUser> userManager
            , ICustomRepositoryServices<SubmittedWritingCorrectionFiles, string> submittedCorrectionFilesRepository
            , ICustomRepositoryServices<WritingCorrectionFile, int> writingCorrectionFileRepository)
        {
            _userManager = userManager;
            _submittedCorrectionFilesRepository = submittedCorrectionFilesRepository;
            _writingCorrectionFileRepository = writingCorrectionFileRepository;
        }
        [Authorize]
        [HttpPost(UploadProcessedWritingFilesRequest.EndpointUri)]
        public override async Task<ActionResult<UploadProcessedWritingFilesRequest.Response>> HandleAsync(UploadProcessedWritingFilesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request.Request.WritingFiles is not null && request.Request.WritingFiles.Count > 0
                    && request.Request.UserID != string.Empty)
                {
                    var user = await _userManager.FindByIdAsync(request.Request.UserID);
                    if (user != null)
                    {
                        var submitID = await _submittedCorrectionFilesRepository.InsertAsync(new SubmittedWritingCorrectionFiles
                        {
                            User = user,
                            UserID = user.Id
                        });
                        var submitRecord = await _submittedCorrectionFilesRepository.FindByIDAsync(submitID);
                        if (submitRecord != null)
                        {
                            var writingCorrectionFiles = new List<WritingCorrectionFile>();
                            foreach (var item in request.Request.WritingFiles)
                            {
                                var writingFile = new WritingCorrectionFile
                                {
                                    Name = item.Name,
                                    Data = item.Data,
                                    WordCount = (int)item.WordCount,
                                    Price = (uint)item.Price,
                                    ApplicationWritingTypeID = item.WritingTypeID,
                                    SubmittedWritingCorrectionFiles = submitRecord,
                                    SubmittedWritingCorecionFilesID = submitRecord.ID,
                                    ApplicationWritingType = null
                                };
                                writingCorrectionFiles.Add(writingFile);
                                var id = await _writingCorrectionFileRepository.InsertAsync(writingFile);
                                writingFile.ID = id;
                            }
                            return Ok(new UploadProcessedWritingFilesRequest.Response(submitRecord.ID));
                        }
                    }
                }
                return Problem("Something went wrong");
            }
            catch (Exception)
            {
                return Problem("Something went wrong");
            }
        }
    }
}
