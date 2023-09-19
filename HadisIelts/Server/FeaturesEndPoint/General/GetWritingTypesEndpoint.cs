using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.General
{
    public class GetWritingTypesEndpoint : EndpointBaseSync
        .WithoutRequest
        .WithActionResult<GetWritingTypesRequest.Response>
    {
        private readonly ICustomRepositoryServices<ApplicationWritingType, int> _writingTypeRepository;
        public GetWritingTypesEndpoint(ICustomRepositoryServices<ApplicationWritingType, int> writingTypeRepository)
        {
            _writingTypeRepository = writingTypeRepository;
        }
        [HttpGet(GetWritingTypesRequest.EndPointUri)]
        public override ActionResult<GetWritingTypesRequest.Response> Handle()
        {
            var result = _writingTypeRepository.GetAll();
            if (result is not null)
            {
                var writingTypes = new List<WritingTypeSharedModel>();
                foreach (var item in result)
                {
                    writingTypes.Add(new WritingTypeSharedModel { ID = item.ID, Name = item.Name });
                }
                return Ok(new GetWritingTypesRequest.Response(writingTypes));
            }
            return Problem(null);
        }

    }
}
