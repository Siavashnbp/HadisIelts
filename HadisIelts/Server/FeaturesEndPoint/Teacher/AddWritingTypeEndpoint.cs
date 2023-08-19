using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Correction;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class AddWritingTypeEndpoint : EndpointBaseAsync
        .WithRequest<AddWritingTypeRequest>
        .WithActionResult<AddWritingTypeRequest.Response>
    {
        private readonly ICustomRepositoryServices<ApplicationWritingType, int> _writingTypeRepository;
        public AddWritingTypeEndpoint(ICustomRepositoryServices<ApplicationWritingType, int> writingTypeRepository)
        {
            _writingTypeRepository = writingTypeRepository;
        }
        [HttpPost(AddWritingTypeRequest.EndPointUri)]
        public override async Task<ActionResult<AddWritingTypeRequest.Response>> HandleAsync(AddWritingTypeRequest request, CancellationToken cancellationToken = default)
        {
            if (request is not null)
            {
                var writingTypeEntity = new ApplicationWritingType { Name = request.WritingName };
                var writingTypeID = await _writingTypeRepository.InsertAsync(writingTypeEntity);
                var result = await _writingTypeRepository.FindByIDAsync(writingTypeID);
                if (result != null)
                {
                    return Ok(new AddWritingTypeRequest.Response(new WritingType
                    {
                        ID = writingTypeID,
                        Name = request.WritingName,
                    }));
                }
                return Problem(null);
            }
            return BadRequest(null);
        }
    }
}
