using HadisIelts.Client.Services.User;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using MediatR;

namespace HadisIelts.Client.Services.Writing
{
    public class ClientWritingServiceProvider : IClientWritingServices
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;
        public ClientWritingServiceProvider(IMediator mediator
            , IUserServices userServices)
        {
            _mediator = mediator;
            _userServices = userServices;
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
