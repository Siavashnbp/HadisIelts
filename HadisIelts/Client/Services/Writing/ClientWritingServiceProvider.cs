using HadisIelts.Client.Features.Teacher.Models;
using HadisIelts.Shared.Requests.Correction;
using HadisIelts.Shared.Requests.Payment;
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

        public async Task<CalculatedWritingCorrectionPayment> GetSubmittedWritingCorrectionFiles(string submissionID)
        {
            var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userID = authenticationState.User.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            if (userID is not null)
            {
                var request = new GetSubmittedWritingCorrectionFilesRequest(new SubmittedWritingFilesIdentifications
                {
                    UserID = userID,
                    SubmissionID = submissionID
                });
                var response = await _mediator.Send(request);
                return response.CalculatedWritingCorrectionPayment;
            }
            return new CalculatedWritingCorrectionPayment
            {
                ProcessedFiles = new List<ProcessedWritingFile>(),
                Message = "User was not found",
                TotalPrice = 0
            };
        }

        public async Task<List<WritingTypeModel>> GetWritingTypesAsync()
        {
            var writingTypes = new List<WritingTypeModel>();
            var result = await _mediator.Send(new GetWritingTypesRequest());
            if (result is not null)
            {
                foreach (var item in result.WritingTypes)
                {
                    writingTypes.Add(new WritingTypeModel
                    {
                        ID = item.ID,
                        Name = item.Name,
                    });
                }
            }
            return writingTypes;
        }
    }
}
