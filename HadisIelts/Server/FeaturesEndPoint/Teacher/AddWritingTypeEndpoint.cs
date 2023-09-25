using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class AddWritingTypeEndpoint : EndpointBaseSync
        .WithRequest<AddWritingTypeRequest>
        .WithActionResult<AddWritingTypeRequest.Response>
    {
        private readonly ICustomRepositoryServices<ApplicationWritingType, int> _writingTypeRepository;
        public AddWritingTypeEndpoint(ICustomRepositoryServices<ApplicationWritingType, int> writingTypeRepository)
        {
            _writingTypeRepository = writingTypeRepository;
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost(AddWritingTypeRequest.EndPointUri)]
        public override ActionResult<AddWritingTypeRequest.Response> Handle(AddWritingTypeRequest request)
        {
            try
            {
                if (request is not null)
                {
                    var writingTypeEntity = new ApplicationWritingType { Name = request.WritingName };
                    var addedWritingType = _writingTypeRepository.Insert(writingTypeEntity);
                    if (addedWritingType != null)
                    {
                        return Ok(new AddWritingTypeRequest.Response(new WritingTypeSharedModel
                        {
                            ID = addedWritingType.ID,
                            Name = request.WritingName,
                        }));
                    }
                    return Problem(null);
                }
                return BadRequest(null);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
