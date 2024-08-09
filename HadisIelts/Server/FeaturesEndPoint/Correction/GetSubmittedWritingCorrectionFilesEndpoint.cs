using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HadisIelts.Server.FeaturesEndPoint.Correction
{
    public class GetSubmittedWritingCorrectionFilesEndpoint : EndpointBaseAsync
        .WithRequest<GetSubmittedWritingCorrectionFilesRequest>
        .WithActionResult<GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        private readonly ICustomRepositoryServices<WritingCorrectionSubmissionGroup, string>
            _submittedWritingCorrectionFilesRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserServices _userServices;
        public GetSubmittedWritingCorrectionFilesEndpoint(IUserServices userServices
            , ApplicationDbContext dbContext
            , ICustomRepositoryServices<WritingCorrectionSubmissionGroup, string> submittedWritingCorrectionFilesRepository)
        {
            _submittedWritingCorrectionFilesRepository = submittedWritingCorrectionFilesRepository;
            _dbContext = dbContext;
            _userServices = userServices;
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
                var submission = await _submittedWritingCorrectionFilesRepository.FindByIdAsync(request.SubmissionId);
                if (submission != null)
                {
                    var isUserAuthorized = _userServices.IsUserOwnerOrSpecificRoles
                        (User.Claims.ToList(), new List<string> { "Teacher", "Administrator" }, submission.UserId);
                    if (isUserAuthorized)
                    {
                        var files = await _dbContext.WritingCorrectionFiles.Where
                            (x => x.WritingCorrectionSubmissionGroupId == submission.Id).ToListAsync();
                        var correctedFiles = await _dbContext.CorrectedWritingFiles.Where
                            (x => x.WritingCorrectionSubmissionGroupId == submission.Id).ToListAsync();
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
                                }, HttpStatusCode.OK));
                        }
                        return NoContent();
                    }
                    return Conflict();
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
