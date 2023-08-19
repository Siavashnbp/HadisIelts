using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class AddWritingCorrectionPriceEndPoint : EndpointBaseAsync
        .WithRequest<AddWritingCorrectionPriceRequest>
        .WithActionResult<AddWritingCorrectionPriceRequest.Response>
    {
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int> _writingCorrectionRepository;
        private readonly ICustomRepositoryServices<ApplicationWritingType, int> _writingTypeRepository;
        public AddWritingCorrectionPriceEndPoint
            (ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionRepository
            , ICustomRepositoryServices<ApplicationWritingType, int> writingTypeRepository)
        {
            _writingCorrectionRepository = writingCorrectionRepository;
            _writingTypeRepository = writingTypeRepository;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(AddWritingCorrectionPriceRequest.EndPointUri)]
        public override async Task<ActionResult<AddWritingCorrectionPriceRequest.Response>> HandleAsync(AddWritingCorrectionPriceRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                //validate request
                if (request.Request is not null)
                {
                    if (request.Request.WritingTypeID != 0)
                    {
                        var writingType = await _writingTypeRepository.FindByIDAsync(request.Request.WritingTypeID);
                        var writingCorrectionPriceEntity = new WritingCorrectionServicePrice
                        {
                            Name = request.Request.Name,
                            Price = request.Request.Price,
                            WordCount = request.Request.WordCount,
                            WritingType = writingType
                        };
                        var id = await _writingCorrectionRepository.InsertAsync(writingCorrectionPriceEntity);
                        var createdEntity = await _writingCorrectionRepository.FindByIDAsync(id);
                        if (createdEntity is not null)
                        {
                            return Ok(new AddWritingCorrectionPriceRequest.Response(new WritingCorrectionPrice
                            {
                                ID = createdEntity.ID,
                                Name = createdEntity.Name,
                                Price = createdEntity.Price,
                                WordCount = createdEntity.WordCount,
                                WritingTypeID = createdEntity.WritingTypeID
                            }));
                        }
                    }
                }
                return Problem();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
