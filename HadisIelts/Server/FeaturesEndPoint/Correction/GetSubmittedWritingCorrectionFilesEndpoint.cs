using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Correction
{
    public class GetSubmittedWritingCorrectionFilesEndpoint : EndpointBaseAsync
        .WithRequest<GetSubmittedWritingCorrectionFilesRequest>
        .WithActionResult<GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public GetSubmittedWritingCorrectionFilesEndpoint(UserManager<ApplicationUser> userManager
            , ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        /// <summary>
        /// gets submitted writing correction files of a user
        /// </summary>
        /// <param name="request">
        /// UserId: Id of issuer user
        /// SubmissionId: Id of submitted group
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// Unauthorized if found user is different and is not in admin or teacher role
        /// WritingCorrectionPackage, with appropriate message
        /// </returns>
        [Authorize]
        [HttpPost(GetSubmittedWritingCorrectionFilesRequest.EndpointUri)]
        public override async Task<ActionResult<GetSubmittedWritingCorrectionFilesRequest.Response>> HandleAsync(GetSubmittedWritingCorrectionFilesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user != null)
                {
                    var isUserTeacher = await _userManager.IsInRoleAsync(user, "Administrator,Teacher");
                    var submission = await _dbContext.WritingCorrectionSubmissionGroups.FindAsync(request.SubmissionId);
                    if (submission != null)
                    {
                        if (!isUserTeacher)
                        {
                            if (submission.UserId != request.UserId)
                            {
                                return Unauthorized();
                            }
                        }
                        var files = _dbContext.WritingCorrectionFiles.ToList().FindAll
                            (x => x.WritingCorrectionSubmissionGroupId == submission.Id);
                        var correctedFiles = _dbContext.CorrectedWritingFiles.ToList().FindAll
                            (x => x.WritingCorrectionSubmissionGroupId == submission.Id);
                        if (files is not null && files.Count > 0)
                        {
                            var writingFiles = new List<ProcessedWritingFileSharedModel>();
                            foreach (var file in files)
                            {
                                var correctedFile = correctedFiles.FirstOrDefault(x => x.WritingCorrectionFileId == file.Id);
                                writingFiles.Add(new ProcessedWritingFileSharedModel
                                {
                                    WritingFile = new WritingFileSharedModel
                                    {
                                        Id = file.Id,
                                        Name = file.Name,
                                        Data = file.Data,
                                        WordCount = file.WordCount,
                                        WritingTypeId = file.ApplicationWritingTypeId
                                    },
                                    PriceGroup = new PriceGroupSharedModel
                                    {
                                        Price = file.Price,
                                        PriceName = file.PriceName
                                    },
                                    CorrectedWriting = correctedFile is not null ? new CorrectedWritingSharedModel
                                    {
                                        Id = correctedFile.Id,
                                        Data = correctedFile.Data,
                                        Name = correctedFile.Name,
                                        UploadDateTime = correctedFile.UploadDateTime,
                                        WritingFileId = correctedFile.WritingCorrectionFileId
                                    } : null
                                });
                            }
                            return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(
                                new WritingCorrectionPackageSharedModel
                                {
                                    Id = submission.Id,
                                    ProcessedWritingFiles = writingFiles,
                                    TotalPrice = submission.TotalPrice,
                                    IsCorrected = submission.IsCorrected,
                                }));
                        }
                        return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(
                            WritingCorrectionPackage: new WritingCorrectionPackageSharedModel
                            {
                                ProcessedWritingFiles = new List<ProcessedWritingFileSharedModel>(),
                                TotalPrice = 0,
                            }));

                    }
                    return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(
                        new WritingCorrectionPackageSharedModel
                        {
                            ProcessedWritingFiles = new List<ProcessedWritingFileSharedModel>(),
                            TotalPrice = 0,
                        }));
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
