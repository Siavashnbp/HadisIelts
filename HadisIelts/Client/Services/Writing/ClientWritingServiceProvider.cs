using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;

namespace HadisIelts.Client.Services.Writing
{
    public class ClientWritingServiceProvider : IClientWritingServices
    {
        private readonly IMediator _mediator;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ClientWritingServiceProvider(IMediator mediator
            , AuthenticationStateProvider authenticationStateProvider)
        {
            _mediator = mediator;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<GetSubmittedWritingCorrectionFilesRequest.Response> GetSubmittedWritingCorrectionFiles(string submissionId)
        {
            var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userId = authenticationState.User.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            if (userId is not null)
            {
                var request = new GetSubmittedWritingCorrectionFilesRequest(
                    UserId: userId,
                    SubmissionId: submissionId
                );
                var response = await _mediator.Send(request);
                return response;
            }
            return new GetSubmittedWritingCorrectionFilesRequest.Response(
            new WritingCorrectionPackageSharedModel
            {
                ProcessedWritingFiles = new List<ProcessedWritingFileSharedModel>(),
                TotalPrice = 0
            }, Message: "User was nor found");
        }

        public async Task<List<WritingTypeSharedModel>> GetWritingTypesAsync()
        {
            var writingTypes = new List<WritingTypeSharedModel>();
            var result = await _mediator.Send(new GetWritingTypesRequest());
            if (result is not null)
            {
                foreach (var item in result.WritingTypes)
                {
                    writingTypes.Add(new WritingTypeSharedModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }
            }
            return writingTypes;
        }
    }
}
