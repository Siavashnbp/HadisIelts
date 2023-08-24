using HadisIelts.Client.Features.Teacher.Models;
using HadisIelts.Shared.Requests.Correction;
using MediatR;

namespace HadisIelts.Client.Services.Writing
{
    public class ClientWritingServiceProvider : IClientWritingServices
    {
        private readonly IMediator _mediator;
        public ClientWritingServiceProvider(IMediator mediator)
        {
            _mediator = mediator;
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
